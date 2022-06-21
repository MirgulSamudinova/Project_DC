using Microsoft.EntityFrameworkCore;
using Project_DC.Models;

namespace Project_DC.Models
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {


        }

        public DbSet<Positions> Positions { get; set; }

        public DbSet<Project_DC.Models.Staffs>? Staffs { get; set; }

        public DbSet<Project_DC.Models.Genders>? Genders { get; set; }

        public DbSet<Project_DC.Models.Rooms>? Rooms { get; set; }

        public DbSet<Project_DC.Models.Patients>? Patients { get; set; }

        public DbSet<Project_DC.Models.Services>? Services { get; set; }

        public DbSet<Project_DC.Models.ServicesGroup>? ServicesGroup { get; set; }

        public DbSet<Project_DC.Models.Schedule>? Schedule { get; set; }

        public DbSet<Project_DC.Models.ScheduleTemplate> ScheduleTemplate { get; set; }
        public DbSet<Project_DC.Models.Event> Events { get; set; }
        public DbSet<Project_DC.Models.Roles> Roles { get; set; }
        public DbSet<Project_DC.Models.DaysOfWeek> DaysOfWeek { get; set; }
        public DbSet<Project_DC.Models.Units> Units { get; set; }
        public DbSet<Project_DC.Models.Materials> Materials { get; set; }


        public DbSet<ToothType>? ToothTypes { get; set; }
        public DbSet<ToothSector>? ToothSectors { get; set; }
        public DbSet<Tooth>? Teeth { get; set; }
        public DbSet<ToothState>? ToothStates { get; set; }
        public DbSet<ClientsTooth>? ClientsTeeth { get; set; }
        public DbSet<DentalService>? DentalServices { get; set; }
        public DbSet<DentalServiceGroup>? DentalServiceGroups { get; set; }
        public DbSet<ToothService>? ToothServices { get; set; }
        public DbSet<GeneralService>? GeneralService { get; set; }
        public DbSet<ClientsService>? ClientsServices { get; set; }

    }
}