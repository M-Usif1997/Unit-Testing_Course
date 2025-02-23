using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingID = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingID = null)
        {
            var unitOfWork = new UnitOfWork();
            var booking = unitOfWork.Query<Booking>().Where(b => b.Status != "Cancelled");

            if (excludedBookingID.HasValue)
                booking = booking.Where(b => b.Id != excludedBookingID.Value);
            return booking;

        }
    }
}
