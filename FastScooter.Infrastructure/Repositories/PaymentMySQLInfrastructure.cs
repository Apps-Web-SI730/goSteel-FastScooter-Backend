using FastScooter.Infrastructure.Context;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FastScooter.Infrastructure.Repositories;

public class PaymentMySQLInfrastructure :IPaymentInfrastructure
{

    private FastScooterContext _context;

    public PaymentMySQLInfrastructure(FastScooterContext context)
    {
        _context = context;
    }


    public bool save(Payment payment)
    {   payment.IsActive = true;
        _context.Payments.Add(payment);
        _context.SaveChanges();
        return true;
    }

    public  async Task<List<Payment>> GetByUserId(int userId)
    {
        return await _context.Payments.Where(payment => payment.UserId == userId).ToListAsync();
    }

    public async Task<List<Payment>> GetByRentId(int rentId)
    {
        return await _context.Payments.Where(payment => payment.RentId == rentId).ToListAsync();
    }

    public bool delete(int id)
    {
        var user = _context.Payments.Find(id);
        user.IsActive = false;
        _context.Payments.Update(user);
        _context.SaveChanges();
        return true;
    }

    public bool existsById(int id)
    {
        return _context.Payments.Any(favorite => favorite.Id == id && favorite.IsActive == true);
    }
}