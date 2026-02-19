using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Data
{
    public class MaintenanceWebAPIContext : DbContext
    {
        public MaintenanceWebAPIContext (DbContextOptions<MaintenanceWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Maintenance.WebAPI.Models.RepairHistoryDto> RepairHistoryDto { get; set; } = default!;
    }
}
