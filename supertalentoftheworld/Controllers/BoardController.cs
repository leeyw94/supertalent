using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Board.Util;
using supertalentoftheworld.Models;
using System.Data.Entity;

namespace SmartFactory.Controllers
{
    public class BoardController : Controller
    {
        private readonly db_e db = new db_e();
        private string Comname = "supertalent";
        // GET: Board

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult BoardAdmin()
        {
            return View();
        }

        [HttpPost]
        public JsonResult BoardAdmin(string id, string pw)
        {
            if (id == "admin" && pw == "1234")
            {
                Session["admin"] = id;
                Session.Timeout = 6000;
                return Json("success");
            }

            return Json("fail");
        }


     
        public ActionResult Board_List(int? cate, int? page)
        {
            string web_company_id = "";
            string web_department_id =  "";
            int auth = 0;

            #region 언어선택
            //언어 추가

            string _language = Request["language"] ?? "korea";

            var language = new HttpCookie("language");
            language.Value = _language;
            Response.Cookies.Add(language);
            #endregion


            string search_all_type = Request["search_all_type"] ?? "";
            string search_all = Request["search_all"] ?? "";

            ViewBag.search_all_type = search_all_type;
            ViewBag.search_all = search_all;

            int _cate = 0;

            if (cate == null)
            {
                _cate = (from a in db.BoardMenu where a.open_yn == "Y" select a.BM_idx).FirstOrDefault();
                cate = _cate;
            }


            //var data = db.BoardMenu.Find(id);


            BoardMenu bm = db.BoardMenu.Find(cate);
            ViewBag.BoardMenu = bm;

            var _type = (from a in db.BoardMenu where a.BM_idx == cate select a).FirstOrDefault() ;

            if (_type != null)
            {
                ViewBag.타입 = _type.BM_type;
            }
            else
            {
                ViewBag.타입 = "normal";
            }

          

            ViewBag.기본게시글 = cate;
            ViewBag.타이틀 = _type.BM_title;

           var board = new supertalentoftheworld.Models.Board();


            if (auth >= 8)
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type != "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new {a.BM_idx, a.BM_title});
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }
            else
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type != "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new {a.BM_idx, a.BM_title});
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }


            if (bm == null)
            {
                var emptyboard = new List<supertalentoftheworld.Models.Board>();


                board.BD_idx = 0;
                board.BD_title = "There's no writing";
                board.BD_writer = "Helper";
                board.BD_wdate = DateTime.Now;
                board.BoardMenu = bm;
                board.BD_BM_idx = 1;
                emptyboard.Add(board);
                return View(emptyboard);
            }
            else
            {
                #region 일반게기판

                IEnumerable<supertalentoftheworld.Models.Board> data = bm.Board.Where(a => a.BD_useable == 1 && a.BoardMenu.BM_type != "photo");

                if (!string.IsNullOrEmpty(search_all))
                {
                    if(search_all_type == "1")
                    {
                        data = data.Where(p => p.BD_title.Contains(search_all) || p.BD_content.Contains(search_all));
                    }
                    if (search_all_type == "2")
                    {
                        data = data.Where(p => p.BD_title.Contains(search_all));
                    }
                    if (search_all_type == "3")
                    {
                        data = data.Where(p => p.BD_writer.StartsWith(search_all) );
                    }

                }

                //페이징 처리
                var pg = new pagination();

                pg.totalcount = data.Any() ? data.Count() : 1;
                pg.takecount = 10;
                pg.skipcount = page == null ? 0 : (int) page*pg.takecount;
                pg.totalpagecount = Math.Ceiling((double) pg.totalcount/pg.takecount);
                data = data.OrderByDescending(a => a.BD_idx).Skip(pg.skipcount).Take(pg.takecount);

                ViewBag.pagination = pg;

                List<supertalentoftheworld.Models.Board> boarddata = data.ToList();
                //게시글이 없을 경우
                if (boarddata.Count() == 0)
                {
                    board.BD_idx = 0;
                    board.BD_title = "There's no writing";
                    board.BD_writer = "Helper";
                    board.BD_wdate = DateTime.Now;
                    board.BoardMenu = bm;
                    board.BD_BM_idx = bm.BM_idx;
                    boarddata.Add(board);
                }
                return View(boarddata);

                #endregion
            }
        }

        [Authorize]
        public ActionResult photo_list(int? cate, int? page)
        {
            string web_company_id = Request.Cookies["web_company_id"].Value ?? "";
            string web_department_id = Request.Cookies["web_department_id"].Value ?? "";
            int auth = Convert.ToInt16(Request.Cookies["check_auth"].Value);
            string search_type = Request["search_type"] ?? "";

            string LoadFileUrl = ConfigurationManager.AppSettings["LoadFileUrl"];
            ViewBag.LoadFileUrl = LoadFileUrl;

            int _cate = 0;

            if (cate == null)
            {
                _cate =
                    (from a in db.BoardMenu
                     where (a.department_id == web_department_id || a.open_yn == "Y")
                     select a.BM_idx).FirstOrDefault();
                cate = _cate;
            }

            if (!string.IsNullOrEmpty(search_type))
            {
                cate = Convert.ToInt32(search_type);

            }
            string search_text = Request["search_text"] ?? "";
            string cate_str = cate.ToString();
            //var data = db.BoardMenu.Find(id);
            ViewBag.search_all = search_text;
            //======================================================================================================================================================== 
            var Code_machine_parts =
                db.project_main.Where(p => p.mode_type == "Y").OrderBy(o => o.project_name).Select(
                    c => new {값 = c.project_id, 이름 = c.project_name});
            ViewBag.세팅 = new SelectList(Code_machine_parts.AsEnumerable(), "값", "이름");
            //======================================================================================================================================================== 




            BoardMenu bm = db.BoardMenu.Find(cate);
            ViewBag.BoardMenu = bm;

            string _type = (from a in db.BoardMenu where a.BM_idx == cate select a.BM_type).FirstOrDefault() ?? "photo";


            ViewBag.타입 = _type;


            var board = new BoardFile();


            if (auth >= 8)
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type == "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new {a.BM_idx, a.BM_title});
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }
            else
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type == "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new {a.BM_idx, a.BM_title});
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }


            if (bm == null)
            {
                var emptyboard = new List<BoardFile>();


                board.Board.BD_BM_idx = 0;
                board.Board.BD_title = "No list.";
                board.Board.BD_writer = "Helper";
                board.Board.BD_wdate = DateTime.Now;
                board.BoardMenu = bm;
                board.BD_BM_idx = 0;
                emptyboard.Add(board);
                return View(emptyboard);
            }
            else
            {
                #region 포토

                IQueryable<BoardFile> data =
                    (from a in db.BoardFile where a.BD_BM_idx == cate && a.BF_useable == 1 && a.BF_type == "P" select a);

                if (!string.IsNullOrEmpty(search_text))
                {

                    data = data.Where(p => p.Board.BD_title.StartsWith(search_text) || p.Board.BD_writer == search_text);

                }

                //페이징 처리
                var pg = new pagination();

                pg.totalcount = data.Any() ? data.Count() : 1;
                pg.takecount = 30;
                pg.skipcount = page == null ? 0 : (int) page*pg.takecount;
                pg.totalpagecount = Math.Ceiling((double) pg.totalcount/pg.takecount);
                data = data.OrderByDescending(a => a.BF_wdate).Skip(pg.skipcount).Take(pg.takecount);

                ViewBag.pagination = pg;
                ViewBag.cate = cate.ToString();

                List<BoardFile> boarddata = data.ToList();

                return View( boarddata);

                #endregion
            }
        }



        [Authorize]
        public ActionResult m_photo_list_mainset()
        {

            string returnUrl = "/board/m_photo_list?cate=7";
            string m_id = Request["m_id"] ?? "";
            int idx = Convert.ToInt32(Request["idx"]);

            if (!string.IsNullOrEmpty(m_id))
            {
                var _update =
                       (from a in db.BoardFile where a.machine_id == m_id select a).ToList();

                foreach (var item in _update)
                {


                    if (item.BF_idx == idx)
                    {
                        item.main_img = "Y";
                    }
                    else
                    {
                        item.main_img = "N";
                    }
                    db.SaveChanges(); // 실제로 저장  
                }

            }

            return Content("<script>alert('Set as representative image');location.href='/board/m_photo_list?search_type="+ m_id + "';</script>");


        

        }

        [Authorize]
        public ActionResult m_photo_list(int? cate, int? page)
        {
            string web_company_id = Request.Cookies["web_company_id"].Value ?? "";
            string web_department_id = Request.Cookies["web_department_id"].Value ?? "";
            int auth = Convert.ToInt16(Request.Cookies["check_auth"].Value);
            string search_type = Request["search_type"] ?? "";

            string LoadFileUrl = ConfigurationManager.AppSettings["LoadFileUrl"];

            ViewBag.LoadFileUrl = LoadFileUrl;

            string machine_id_search = search_type;

            cate = 7;

            //int _cate = 0;

            //if (cate == null)
            //{
            //    _cate =
            //        (from a in db.BoardMenu
            //         where (a.department_id == web_department_id || a.open_yn == "Y")
            //         select a.BM_idx).FirstOrDefault();
            //    cate = _cate;
            //}

            //if (!string.IsNullOrEmpty(search_type))
            //{
            //    cate = Convert.ToInt32(search_type);

            //}
            string search_text = Request["search_text"] ?? "";
            string cate_str = cate.ToString();

            ViewBag.search_all = search_text;
            ViewBag.search_type = machine_id_search;
            //var data = db.BoardMenu.Find(id);

            //======================================================================================================================================================== 
            var Code_machine_parts =
                db.project_main.Where(p => p.mode_type == "Y" && p.use_yn !="D").OrderBy(o => o.project_name).Select(
                    c => new { 값 = c.project_id, 이름 = c.project_name });
            ViewBag.세팅 = new SelectList(Code_machine_parts.AsEnumerable(), "값", "이름");
            //======================================================================================================================================================== 




            BoardMenu bm = db.BoardMenu.Find(cate);
            ViewBag.BoardMenu = bm;

            string _type = (from a in db.BoardMenu where a.BM_idx == cate select a.BM_type).FirstOrDefault() ?? "photo";


            ViewBag.타입 = _type;


            var board = new BoardFile();


            if (auth >= 8)
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type == "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new { a.BM_idx, a.BM_title });
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }
            else
            {
                //회사별 데이터 드롭다운==============================================================================================================================================          
                var category =
                    db.BoardMenu.Where(
                        a =>
                        a.BM_com == web_company_id && a.BM_type == "photo" &&
                        ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                            a => new { a.BM_idx, a.BM_title });
                ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
                //=====================================================================================================================================================================
            }


            if (bm == null)
            {
                var emptyboard = new List<BoardFile>();


                board.Board.BD_BM_idx = 0;
                board.Board.BD_title = "게시글이 없습니다.";
                board.Board.BD_writer = "마스터";
                board.Board.BD_wdate = DateTime.Now;
                board.BoardMenu = bm;
                board.BD_BM_idx = 0;
                emptyboard.Add(board);
                return View(emptyboard);
            }
            else
            {
                #region 포토

                IQueryable<BoardFile> data =
                    (from a in db.BoardFile where a.BD_BM_idx == cate && a.BF_useable == 1 && a.BF_type == "P" select a);

                if (!string.IsNullOrEmpty(machine_id_search))
                {

                    data = data.Where(p => p.machine_id  == machine_id_search);

                }
              
                if (!string.IsNullOrEmpty(search_text))
                {

                    data = data.Where(p => p.Board.BD_title.StartsWith(search_text) || p.Board.BD_writer == search_text);

                }




                //페이징 처리
                var pg = new pagination();

                pg.totalcount = data.Any() ? data.Count() : 1;
                pg.takecount = 30;
                pg.skipcount = page == null ? 0 : (int)page * pg.takecount;
                pg.totalpagecount = Math.Ceiling((double)pg.totalcount / pg.takecount);
                data = data.OrderByDescending(a => a.BF_wdate).Skip(pg.skipcount).Take(pg.takecount);

                ViewBag.pagination = pg;
                ViewBag.cate = cate.ToString();

                List<BoardFile> boarddata = data.ToList();

                return View(boarddata);

                #endregion
            }
        }
       
        public ActionResult BoardView(int? idx)
        {
            if (idx == null)
            {
                return HttpNotFound();
            }
            else if (idx == 0)
            {
                //0번 게시글은 임시글로써, DB에 존재하지 않는 글임
                return Content("<script>alert('사용할 수 없는 게시물입니다.');history.back();</script>");
            }


          




            supertalentoftheworld.Models.Board data = db.Board.Find(idx);

            string file_bbs =
                (from a in db.BoardFile where a.BF_BD_idx == idx select a.department_id).FirstOrDefault() ?? "";
            ViewBag.file_dir = file_bbs;

            data.BD_hit += 1;
            db.SaveChanges();

            //TODO :: 코멘트, 파일 가져와야함 뷰단에서


            IQueryable<bbs_comment> _list = Enumerable.Empty<bbs_comment>().AsQueryable();

            _list =  db.bbs_comment.Where(p => p.BD_idx == idx && p.use_yn == "Y").OrderByDescending(o => o.idx);

            ViewBag.댓글 = _list;



            return View(data);
        }

        [Authorize]
        public ActionResult BoardWrite(int? idx, int? cate)
        {
            var board = new supertalentoftheworld.Models.Board();
            int auth = Convert.ToInt16(Request.Cookies["check_auth"].Value);
            //첫글 임시작성
            var bmtemp = new BoardMenu();
            bmtemp.BM_url = "/Board/Index";
            board.BoardMenu = bmtemp;
            if (cate != null)
            {
                BoardMenu bm = db.BoardMenu.Find(cate);
                board.BD_BM_idx = bm.BM_idx;
                board.BoardMenu = bm;
            }


            if (idx != null)
            {
                //수정모드
                board = db.Board.Find(idx);
            }


            //회사별 데이터 드롭다운==============================================================================================================================================
            string web_department_id = Request.Cookies["web_department_id"].Value ?? "";
            var category =
                db.BoardMenu.Where(
                    a => a.BM_com == Comname && a.BM_type != "photo" && ((a.department_id == web_department_id || a.open_yn == "Y"))).Select(
                        a => new {a.BM_idx, a.BM_title});

            ViewBag.category = new SelectList(category.AsEnumerable(), "BM_idx", "BM_title", board.BD_BM_idx);
            //=====================================================================================================================================================================

          



            return View(board);
        }


        public ActionResult BbsComemt_action(bbs_comment doc)
        {
            int idx = 0;
            string b_idx =Request["BD_idx"];
            string cate = Request["cate"];

            string mode_type = Request["mode_type"];
            string msg = "";

            string company_id = Request.Cookies["web_company_id"].Value ?? "insstek";
            string web_company_id = Request.Cookies["web_company_id"].Value ?? "insstek";

         

            if (string.IsNullOrEmpty(Request["c_idx"]))
            {
                #region 저장

                doc.write_date = DateTime.Now;
                doc.writer = User.Identity.Name;
                doc.edit_date = DateTime.Now;
                doc.use_yn = "Y";
                doc.user_ip = Request.UserHostAddress;
                db.bbs_comment.Add(doc);
                db.SaveChanges(); // 실제로 저장 


                msg = Util.msg.msg_insert;

                #endregion
            }
            else
            {
                idx = Convert.ToInt32(Request["c_idx"]);

                if (mode_type == "D")
                {
                    #region 삭제

                    bbs_comment doc_del = db.bbs_comment.Single(x => x.idx == idx);
                    db.bbs_comment.Remove(doc_del);
                    db.SaveChanges();

                    msg = Util.msg.msg_del;

                    #endregion
                }

                else if (mode_type == "E")
                {
                    #region 임시 삭제 / 상태 변환 업데이트

                    bbs_comment _update =
                        (from a in db.bbs_comment where a.idx == idx select a).Single();
                    _update.edit_date = DateTime.Now;
                    _update.use_yn = "D";
                    _update.writer = User.Identity.Name;
                    _update.user_ip = Request.UserHostAddress;
                  
                    db.SaveChanges(); // 실제로 저장 


                    msg = Util.msg.msg_disable;

                    #endregion
                }
                else
                {
                    #region 수정


                    bbs_comment _update =
                        (from a in db.bbs_comment where a.idx == idx select a).Single();

                    _update.edit_date = DateTime.Now;          
                    _update.user_ip = Request.UserHostAddress;
                    _update.memo = doc.memo;
                  

                    db.SaveChanges(); // 실제로 저장 



                    msg = Util.msg.msg_edit;

                    #endregion
                }
            }

            return
                Content("<html><script>alert('" + msg +
                        "'); window.top.location.href ='/board/boardview?idx="+b_idx+"&cate="+cate+"';</script></html>");
        }

        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult CreateBoard(supertalentoftheworld.Models.Board board)
        {
            int cate = board.BD_BM_idx ;




            if (ModelState.IsValid)
            {
                board.BD_wdate = DateTime.Now;
                board.BD_hit = 0;
                board.BD_useable = 1;
                board.BD_writer = User.Identity.Name;

                db.Board.Add(board);
            }

            var fm = new FileManager();
            List<string> files = fm.FileUpload();

            int BD_idx = db.Board.Any() ? db.Board.Max(a => a.BD_idx) + 1 : 1;

            foreach (string file in files)
            {
                var bf = new BoardFile();
                bf.BF_BD_idx = BD_idx;
                bf.BF_name = file;
                bf.BF_useable = 1;
                bf.BF_wdate = DateTime.Now;


                db.BoardFile.Add(bf);
            }
            db.SaveChanges();


            string returnUrl = "/board/board_list?cate=" + cate;

            return Redirect(returnUrl);


            //return Content("<script>location.href='"+ returnUrl + "';</script>");
        }


        [HttpPost]
        public ActionResult photo_action()
        {
            int cate = Convert.ToInt32(Request["cate"]);
            string web_department_id = Request.Cookies["web_department_id"].Value ?? "";
            string machine_id = Request["machine_id"] ?? "";
            int project_id = 0;

            if (!string.IsNullOrEmpty(Request["project_id"]))
            {
                project_id = Convert.ToInt32((Request["project_id"]));
            }


            HttpRequest httpRequest = System.Web.HttpContext.Current.Request;

            int file_count = httpRequest.Files.Count;
            string title = Request["title"] ?? "";


            var fm = new FileManager();
            List<string> files = fm.PhotoFileUpload(web_department_id);


            int count = 0;
            foreach (string file in files)
            {
                #region 포토게시판 기록 

                int BD_idx = db.Board.Any() ? db.Board.Max(a => a.BD_idx) + 1 : 1;

                var _insert = new supertalentoftheworld.Models.Board
                                  {
                                      BD_BM_idx = cate,
                                      BD_title = title,
                                      BD_writer = User.Identity.Name,
                                      BD_hit = 0,
                                      BD_wdate = DateTime.Now,
                                      BD_useable = 1,
                                      machine_id = machine_id,
                                      project_id = project_id
                                  };
                db.Board.Add(_insert);
                db.SaveChanges(); // 실제로 저장  

                #endregion

                var bf = new BoardFile();
                bf.BF_BD_idx = BD_idx;
                bf.BF_name = file;
                bf.BF_useable = 1;
                bf.BF_wdate = DateTime.Now;
                bf.BF_type = "P";
                bf.BD_BM_idx = cate;
                bf.department_id = web_department_id;
                db.BoardFile.Add(bf);


                count++;
            }
            db.SaveChanges();

            //


            string returnUrl = "/board/photo_list?cate=" + cate;

            return Redirect(returnUrl);


            //return Content("<script>location.href='"+ returnUrl + "';</script>");
        }
        [HttpPost]
        public ActionResult m_photo_action()
        {
            int cate = 7;
            string web_department_id = Request.Cookies["web_department_id"].Value ?? "";
            string machine_id = Request["machine_id"] ?? "";
            int project_id = 0;

            if (!string.IsNullOrEmpty(Request["project_id"]))
            {
                project_id = Convert.ToInt32((Request["project_id"]));
            }


            HttpRequest httpRequest = System.Web.HttpContext.Current.Request;

            int file_count = httpRequest.Files.Count;
            string title = Request["title"] ?? "";


            var fm = new FileManager();
            List<string> files = fm.PhotoFileUpload(web_department_id);


            int count = 0;
            foreach (string file in files)
            {
                #region 포토게시판 기록 

                int BD_idx = db.Board.Any() ? db.Board.Max(a => a.BD_idx) + 1 : 1;

                var _insert = new supertalentoftheworld.Models.Board
                {
                    BD_BM_idx = cate,
                    BD_title = title,
                    BD_writer = User.Identity.Name,
                    BD_hit = 0,
                    BD_wdate = DateTime.Now,
                    BD_useable = 1,
                    machine_id = machine_id,
                    project_id = project_id
                };
                db.Board.Add(_insert);
                db.SaveChanges(); // 실제로 저장  

                #endregion

                var bf = new BoardFile();
                bf.BF_BD_idx = BD_idx;
                bf.BF_name = file;
                bf.BF_useable = 1;
                bf.BF_wdate = DateTime.Now;
                bf.BF_type = "P";
                bf.BD_BM_idx = cate;
                bf.department_id = web_department_id;
                bf.machine_id = machine_id;
                bf.main_img = "N";
                bf.project_id = project_id;
                bf.user_id = User.Identity.Name;
                bf.file_title = title;


                db.BoardFile.Add(bf);


                count++;
            }
            db.SaveChanges();

            //


            string returnUrl = "/board/m_photo_list?cate=7";

            return Redirect(returnUrl);


            //return Content("<script>location.href='"+ returnUrl + "';</script>");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateBoard(supertalentoftheworld.Models.Board board)
        {
        
            int cate = board.BD_BM_idx;
            //기존 파일 변동사항을 파악한다
            string oriFileIdxs = Request["oriFileIdx"];

            List<int> oriFileIdxList = oriFileIdxs != null ? oriFileIdxs.Split(',').Select(Int32.Parse).ToList() : null;

            supertalentoftheworld.Models.Board oriBoard = db.Board.Find(board.BD_idx);

            //DB에 원래 있던 파일들
            IEnumerable<BoardFile> dbfiles = oriBoard.BoardFile.Where(a => a.BF_useable == 1);

            foreach (BoardFile dbfile in dbfiles)
            {
                //변경된 파일에도 포함되어 있으면 그대로 유지, 아니면 useable = 0으로 변경
                if (!oriFileIdxList.Contains(dbfile.BF_idx))
                {
                    dbfile.BF_useable = 0;
                }
            }

            // 새로 들어온 파일들 체크
            var fm = new FileManager();
            List<string> files = fm.FileUpload();
            foreach (string file in files)
            {
                var bf = new BoardFile();
                bf.BF_BD_idx = board.BD_idx;
                bf.BF_name = file;
                bf.BF_useable = 1;
                bf.BF_wdate = DateTime.Now;

                db.BoardFile.Add(bf);
            }

            //게시판 내용들 업데이트
            board.BD_edate = DateTime.Now;

            supertalentoftheworld.Models.Board updateboard = db.Board.Find(board.BD_idx);

            updateboard.BD_BM_idx = board.BD_BM_idx;
            updateboard.BD_title = board.BD_title;
            updateboard.BD_writer = User.Identity.Name;
            updateboard.BD_password = board.BD_password;
            updateboard.BD_content = board.BD_content;
            updateboard.BD_edate = DateTime.Now;


            db.SaveChanges();


            string returnUrl = "/board/board_list?cate=" + cate;

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult DelBoard(int? idx, string cate)
        {
            if (idx == null)
            {
                return HttpNotFound();
            }

            supertalentoftheworld.Models.Board board = db.Board.Find(idx);
            BoardMenu bm = board.BoardMenu;
            board.BD_useable = 0;
            db.SaveChanges();


            string BM_url = "/board/board_list?cate=" + cate;

            return Content("<script>alert('OK'); location.href='" + BM_url + "';</script>");
        }


        public ActionResult Photo_Del(int? idx, string cate)
        {
            if (idx == null)
            {
                return HttpNotFound();
            }
            string msg = "";
            BoardFile board = db.BoardFile.Find(idx);

            #region 권한체크

            string user = User.Identity.Name;
            int auth = 0;
            try
            {
                auth = Convert.ToInt16(Request.Cookies["check_auth"].Value);
            }
            catch
            {
            }

            string ok_auth = "N";

            DateTime _today = DateTime.Today;
            DateTime _wdate = board.Board.BD_wdate;

            TimeSpan ts = _today - _wdate;
            int days = Convert.ToInt32(ts.TotalDays);

            if (board.Board.BD_writer == user && days <= 5)
            {
                ok_auth = "Y";
            }
            if (auth >= 8)
            {
                ok_auth = "Y";
            }


          



            #endregion

            if (ok_auth == "Y")
            {
                board.BF_useable = 0;
                db.SaveChanges();
                msg = Util.msg.msg_del;
            }
            else
            {
                msg = Util.msg.msg_no;
            }


            string BM_url = "/board/photo_list?cate=" + cate;

            return Content("<script>alert('" + msg + "'); location.href='" + BM_url + "';</script>");
        }

        public ActionResult m_Photo_Del(int? idx, string cate)
        {
            if (idx == null)
            {
                return HttpNotFound();
            }
            string msg = "";
            BoardFile board = db.BoardFile.Find(idx);

            #region 권한체크

            string user = User.Identity.Name;
            int auth = 0;
            try
            {
                auth = Convert.ToInt16(Request.Cookies["check_auth"].Value);
            }
            catch
            {
            }

            string ok_auth = "N";

            DateTime _today = DateTime.Today;
            DateTime _wdate = board.Board.BD_wdate;

            TimeSpan ts = _today - _wdate;
            int days = Convert.ToInt32(ts.TotalDays);

            if (board.Board.BD_writer == user && days <= 5)
            {
                ok_auth = "Y";
            }
            if (auth >= 8)
            {
                ok_auth = "Y";
            }






            #endregion

            if (ok_auth == "Y")
            {
                board.BF_useable = 0;
                db.SaveChanges();
                msg = Util.msg.msg_del;
            }
            else
            {
                msg = Util.msg.msg_no;
            }


            string BM_url = "/board/m_photo_list?cate=7" ;

            return Content("<script>alert('" + msg + "'); location.href='" + BM_url + "';</script>");
        }





        //게시판 관리자 권한 요청
        [HttpPost]
        public ActionResult GetAdminPermission(BoardAdmin ba)
        {
            if (ba.id == "admin" && ba.pw == "1234")
            {
                Session["boardauth"] = "Admin";
                Session.Timeout = 6000;
            }

            return Redirect(ba.redirect);
        }

        //사용자 게시물 수정 요청
        [HttpPost]
        public ActionResult GetUserPermission(BoardUser bu)
        {
            supertalentoftheworld.Models.Board board = db.Board.Find(bu.board_idx);

            if (board.BD_writer == bu.id && board.BD_password == bu.pw)
            {
                return RedirectToAction("BoardWrite", new {id = bu.board_idx});
            }
            else
            {
                if (board.BoardMenu.BM_type == "Free")
                {
                    if (Session["boardauth"].ToString() == "Admin")
                    {
                        return RedirectToAction("BoardWrite", new {id = bu.board_idx});
                    }
                    else
                    {
                        return
                            Content("<script>alert('Check ID or Password'); location.href='" + bu.redirect + "';</script>");
                    }
                }
                else if (board.BoardMenu.BM_type == "Admin")
                {
                    return RedirectToAction("BoardWrite", new {id = bu.board_idx});
                }

                return Content("<script>alert('Check ID or Password'); location.href='" + bu.redirect + "';</script>");
            }
        }


        [HttpPost]
        public ActionResult BoardImageUpload()
        {
            string CKEnum = Request["CKEditorFuncNum"];
            HttpRequest httpRequest = System.Web.HttpContext.Current.Request;
            HttpFileCollection uploadFiles = httpRequest.Files;
            if (httpRequest.Files.Count > 0)
            {
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                HttpPostedFile postedFile = uploadFiles[0];
                //파일 이름만 좀 수정해주면 될듯
                string filePath = ConfigurationManager.AppSettings["SaveFilePath"];
                var dInfo = new DirectoryInfo(filePath);
                //경로에 폴더가 없으면 만들어준다.
                if (!dInfo.Exists)
                {
                    dInfo.Create();
                }

                string uploadfilename = Path.GetFileName(postedFile.FileName);
                if (string.IsNullOrEmpty(uploadfilename))
                {
                }

                string SavePath = filePath + time + uploadfilename;
                postedFile.SaveAs(SavePath);

                string filename = time + uploadfilename;


                return
                    Content("<script type = 'text/javascript'> window.parent.CKEDITOR.tools.callFunction('" + CKEnum +
                            "', '" + ConfigurationManager.AppSettings["LoadFileUrl"] + filename +
                            "', 'OK'); history.go(-1);</script>");
            }

            return
                Content("< script type = 'text/javascript' > window.parent.CKEDITOR.tools.callFunction('" + 1 + "', '" +
                        ConfigurationManager.AppSettings["LoadFileUrl"] + "', 'Fail'); history.go(-1);</script>");
        }

        public void Fdown(string name, string file_dir)
        {
            file_dir = file_dir + "/";
            string path = ConfigurationManager.AppSettings["SaveFilePath"] + file_dir + name;

            string FileName = HttpUtility.UrlEncode(name, Encoding.UTF8).Replace("+", "%20");

            //string FileName = name.Replace(" ", "_");

            byte[] bts = System.IO.File.ReadAllBytes(path);
            Response.Clear();
            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName.Substring(14));
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();
        }
    }

    public class BoardUser
    {
        public int board_idx { get; set; }
        public string id { get; set; }
        public string pw { get; set; }
        public string redirect { get; set; }
    }

    public class BoardAdmin
    {
        public string id { get; set; }
        public string pw { get; set; }
        public string redirect { get; set; }
    }


    public class pagination
    {
        public int totalcount { get; set; }
        public int page { get; set; }
        public int takecount { get; set; }
        public int skipcount { get; set; }
        public double totalpagecount { get; set; }
    }
}