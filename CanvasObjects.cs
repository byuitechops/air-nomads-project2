using System;
using System.Collections.Generic;

namespace CanvasObjects
{
    public class Module_Item
    {
        public int id { get; set; }
        public string title { get; set; }
        public int position { get; set; }
        public int indent { get; set; }
        public string type { get; set; }
        public int module_id { get; set; }
        public string html_url { get; set; }
        public int content_id { get; set; }
        public string url { get; set; }
        public bool published { get; set; }
    }

    public class Module
    {
        public int id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public object unlock_at { get; set; }
        public bool require_sequential_progress { get; set; }
        public bool publish_final_grade { get; set; }
        public List<object> prerequisite_module_ids { get; set; }
        public bool published { get; set; }
        public int items_count { get; set; }
        public string items_url { get; set; }

        public List<Module_Item> Module_Items {get;set;}
    }

    public class Calendar
    {
        public string ics { get; set; }
    }

    public class Enrollment
    {
        public string type { get; set; }
        public string role { get; set; }
        public int role_id { get; set; }
        public int user_id { get; set; }
        public string enrollment_state { get; set; }
    }

    public class Course
    {
        public int id { get; set; }
        public string name { get; set; }
        public int account_id { get; set; }
        public string uuid { get; set; }
        public object start_at { get; set; }
        public object grading_standard_id { get; set; }
        public object is_public { get; set; }
        public DateTime created_at { get; set; }
        public string course_code { get; set; }
        public string default_view { get; set; }
        public int root_account_id { get; set; }
        public int enrollment_term_id { get; set; }
        public object license { get; set; }
        public object end_at { get; set; }
        public bool public_syllabus { get; set; }
        public bool public_syllabus_to_auth { get; set; }
        public int storage_quota_mb { get; set; }
        public bool is_public_to_auth_users { get; set; }
        public bool apply_assignment_group_weights { get; set; }
        public Calendar calendar { get; set; }
        public string time_zone { get; set; }
        public bool blueprint { get; set; }
        public object sis_course_id { get; set; }
        public object integration_id { get; set; }
        public List<Enrollment> enrollments { get; set; }
        public bool hide_final_grades { get; set; }
        public string workflow_state { get; set; }
        public bool restrict_enrollments_to_course_dates { get; set; }

         public List<Module> Modules {get;set;}
    }

}