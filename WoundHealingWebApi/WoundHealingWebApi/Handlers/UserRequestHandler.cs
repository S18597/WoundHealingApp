using MediatR;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WoundHealingDb;
using WoundHealingDb.Models;
using WoundHealingWebApi.Enums;
using WoundHealingWebApi.Requests;

namespace WoundHealingWebApi.Handlers
{
    public class UserRequestHandler : IRequestHandler<CreateUserRequest>
    {
        public static Logger Log => LogManager.GetLogger("UserRequestHandler");

        private readonly WoundHealingContext _context;

        public UserRequestHandler(WoundHealingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // check if user exists
                var user = await _context.User.Where(u => u.EmailAddress == request.EmailAddress).FirstOrDefaultAsync();
                if(user != null)
                {
                    Log.Error($"User with email: {user.EmailAddress} already exists!");
                    throw new Exception("User already exists!");
                }

                // create user
                var newUser = new User
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    EmailAddress = request.EmailAddress,
                    Pesel = request.Pesel,
                    DateOfBirth = request.DateOfBirth,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    IsPatient = request.AccountType == AccountType.Patient,
                    IsDoctor = request.AccountType == AccountType.Doctor
                };

                await _context.User.AddAsync(newUser);
                await _context.SaveChangesAsync();

                // create auth
                var auth = new Auth
                {
                    UserId = newUser.UserId,
                    Salt = request.Salt,
                    Hash = request.Hash
                };

                await _context.Auth.AddAsync(auth);
                await _context.SaveChangesAsync();

                Log.Info($"User created: {user}");

                return await Task.FromResult(Unit.Value);
            }
            catch(Exception ex)
            {
                Log.Error($"CreateUser error: {ex}");
                throw;
            }
        }
    }
}