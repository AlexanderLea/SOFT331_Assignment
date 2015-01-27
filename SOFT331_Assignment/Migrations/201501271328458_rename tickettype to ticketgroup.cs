namespace SOFT331_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renametickettypetoticketgroup : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TicketTypes", newName: "TicketGroups");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TicketGroups", newName: "TicketTypes");
        }
    }
}
