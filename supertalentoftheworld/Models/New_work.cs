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
    
    public partial class New_work
    {
        public int idx { get; set; }
        public string user_id { get; set; }
        public int project_id { get; set; }
        public double spend_hour { get; set; }
        public Nullable<System.TimeSpan> spend_time { get; set; }
        public string memo { get; set; }
        public System.DateTime write_date { get; set; }
        public System.DateTime edit_date { get; set; }
    
        public virtual project_main project_main { get; set; }
        public virtual user user { get; set; }
    }
}
