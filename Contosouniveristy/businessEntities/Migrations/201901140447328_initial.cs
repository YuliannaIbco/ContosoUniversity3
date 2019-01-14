namespace businessEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Credits = c.Int(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true);
                //.Index(t => t.DepartmentId);
                Sql("CREATE index `DepartmentId` on `Course` (`DepartmentId` DESC)");

            CreateTable(
                "dbo.Department",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 35, storeType: "nvarchar"),
                    Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StartDate = c.DateTime(nullable: false, precision: 0),
                    InstructorId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true);
                //.Index(t => t.InstructorId);
                Sql("CREATE index `InstructorId` on `Department` (`InstructorId` DESC)");

            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 35, storeType: "nvarchar"),
                        FirstMidName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        HireDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    InstructorId = c.Int(nullable: false),
                    Location = c.String(nullable: false, maxLength: 40, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true);
            //.Index(t => t.InstructorId);
            Sql("CREATE index `InstructorId` on `OfficeAssignment` (`InstructorId` DESC)");

            CreateTable(
                "dbo.Enrollment",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CourseId = c.Int(nullable: false),
                    StudentId = c.Int(nullable: false),
                    Grade = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true);
            //.Index(t => t.CourseId)
            //.Index(t => t.StudentId);
            Sql("CREATE index `CourseId` on `Enrollment` (`CourseId` DESC)");
            Sql("CREATE index `StudentId` on `Enrollment` (`StudentId` DESC)");

            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 35, storeType: "nvarchar"),
                        FirstMidName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        EnrollmentDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorId", "dbo.Instructor");
            DropIndex("dbo.Enrollment", new[] { "StudentId" });
            DropIndex("dbo.Enrollment", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorId" });
            DropIndex("dbo.Department", new[] { "InstructorId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            DropTable("dbo.Student");
            DropTable("dbo.Enrollment");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
