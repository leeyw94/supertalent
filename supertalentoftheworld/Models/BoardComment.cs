//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace supertalentoftheworld.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class BoardComment
    {
        public int BC_idx { get; set; }
        public int BC_BD_idx { get; set; }
        public string BC_content { get; set; }
        public System.DateTime BC_wdate { get; set; }
        public System.DateTime BC_edate { get; set; }
        public Nullable<System.DateTime> BC_ddate { get; set; }
        public int BC_good { get; set; }
        public int BC_worst { get; set; }
        public int BC_parent { get; set; }
        public int BC_useable { get; set; }
    
        public virtual Board Board { get; set; }
    }
}
