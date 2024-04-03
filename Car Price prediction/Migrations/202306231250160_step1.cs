namespace Car_Price_prediction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class step1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShowroomReviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        data = c.DateTime(nullable: false),
                        Message = c.String(),
                        phoneNumber = c.String(),
                        UserID = c.String(maxLength: 128),
                        ShowroomID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShowroomsForTraders", t => t.ShowroomID, cascadeDelete: true)
                .ForeignKey("dbo.IdentityUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ShowroomID);
            
            CreateTable(
                "dbo.user_cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsertionDate = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        CarID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarID, cascadeDelete: true)
                .ForeignKey("dbo.IdentityUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.CarID);
            
            AddColumn("dbo.Cars", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.user_cars", "UserID", "dbo.IdentityUsers");
            DropForeignKey("dbo.user_cars", "CarID", "dbo.Cars");
            DropForeignKey("dbo.ShowroomReviews", "UserID", "dbo.IdentityUsers");
            DropForeignKey("dbo.ShowroomReviews", "ShowroomID", "dbo.ShowroomsForTraders");
            DropIndex("dbo.user_cars", new[] { "CarID" });
            DropIndex("dbo.user_cars", new[] { "UserID" });
            DropIndex("dbo.ShowroomReviews", new[] { "ShowroomID" });
            DropIndex("dbo.ShowroomReviews", new[] { "UserID" });
            DropColumn("dbo.Cars", "Active");
            DropTable("dbo.user_cars");
            DropTable("dbo.ShowroomReviews");
        }
    }
}
