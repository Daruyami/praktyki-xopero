using System;
using SQLite;

namespace SQLiteArticleManager
{
    [Table("articles")]
    public class Article
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public uint Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }
        
        [Column("category")]
        public string Category { get; set; }
        
        [Column("creation")]
        public DateTime CreationDate { get; set; }
        
        [Column("modification")]
        public DateTime ModificationDate { get; set; }
        
        [Column("content")]
        public string Content { get; set; }
    }
}