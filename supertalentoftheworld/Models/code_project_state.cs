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
    
    public partial class code_project_state
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public code_project_state()
        {
            this.work_list = new HashSet<work_list>();
        }
    
        public int code_id { get; set; }
        public string code_name { get; set; }
        public string use_yn { get; set; }
        public int index_order { get; set; }
        public string gubun { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<work_list> work_list { get; set; }
    }
}
