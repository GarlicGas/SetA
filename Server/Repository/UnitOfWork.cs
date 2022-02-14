using SetA.Server.Data;
using SetA.Server.IRepository;
using SetA.Server.Models;
using SetA.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentalManagement.Server.Repository;

namespace SetA.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Expenses> _expensess;
        private IGenericRepository<PaymentMethod> _paymentmethods;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Expenses> Expensess
            => _expensess ??= new GenericRepository<Expenses>(_context);
        public IGenericRepository<PaymentMethod> PaymentMethods
             => _paymentmethods ??= new GenericRepository<PaymentMethod>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user1 = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).Name = user1.UserName;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}