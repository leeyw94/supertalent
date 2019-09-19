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
    
    public partial class project_main
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public project_main()
        {
            this.Board = new HashSet<Board>();
            this.BoardFile = new HashSet<BoardFile>();
            this.cal_memo = new HashSet<cal_memo>();
            this.my_work = new HashSet<my_work>();
            this.New_work = new HashSet<New_work>();
            this.project_act = new HashSet<project_act>();
            this.project_dir = new HashSet<project_dir>();
            this.work_list = new HashSet<work_list>();
        }
    
        public int project_id { get; set; }
        public Nullable<int> project_id_name { get; set; }
        public string company_id { get; set; }
        public string owner_id { get; set; }
        public string project_name { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public int state { get; set; }
        public string memo { get; set; }
        public System.DateTime write_date { get; set; }
        public string writer { get; set; }
        public string use_yn { get; set; }
        public string department_id { get; set; }
        public int index_order { get; set; }
        public string project_code { get; set; }
        public string file_sn { get; set; }
        public string mode_type { get; set; }
        public string user_ip { get; set; }
        public string machine_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Board> Board { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardFile> BoardFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cal_memo> cal_memo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<my_work> my_work { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<New_work> New_work { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_act> project_act { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<project_dir> project_dir { get; set; }
        public virtual project_main project_main1 { get; set; }
        public virtual project_main project_main2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<work_list> work_list { get; set; }
    }
}
