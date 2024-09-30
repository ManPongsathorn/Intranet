﻿namespace Intranet.Models
{
    public class Log_Section
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int Department1Id { get; set; }
        public int? Department2Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public string ActionByFirstName { get; set; }
        public string ActionByLastName { get; set; }
        public DateTime ActionOn { get; set; }
        public string? Remark { get; set; }
    }
}
