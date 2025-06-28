using HotelReservation.DataAccess.Models;
using HotelReservationSystem.BL.DTOs.Hotel;
using HotelReservationSystem.BL.DTOs.Payment;
using HotelReservationSystem.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBaseRepository<Payment> _paymentRepository;
        public PaymentController(IBaseRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAllAsync();
            if (payments == null || payments.Count() == 0)
            {
                return NotFound("No Payments found.");
            }
            return Ok(payments);
        }
        [HttpGet("GetById{id:int}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentRepository.FindAsync(p => p.Id == id);
            if (payment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            return Ok(payment);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePayment(addPaymentDto paymentDto)
        {
            Payment payment = new Payment
            {
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentDate = DateTime.Now,
                Status = paymentDto.Status,
            };
            if (payment == null)
            {
                return BadRequest("Payment data is null.");
            }
            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveAsync();
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdatePayment(int id, addPaymentDto paymentDto)
        {
            var existingPayment = await _paymentRepository.FindAsync(p => p.Id == id);
            if (existingPayment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            existingPayment.Amount = paymentDto.Amount;
            existingPayment.PaymentMethod = paymentDto.PaymentMethod;
            existingPayment.PaymentDate = DateTime.Now;
            existingPayment.Status = paymentDto.Status;
            Payment updatedPayment=_paymentRepository.Update(existingPayment);
            if (updatedPayment == null)
            {
                return BadRequest("Failed to update payment.");
            }
            await _paymentRepository.SaveAsync();
            return Ok($"The Payment with ID {existingPayment.Id} Updated Successfully");
        }
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentRepository.FindAsync(p => p.Id == id);
            if (payment == null)
            {
                return NotFound($"Payment with ID {id} not found.");
            }
            _paymentRepository.Delete(payment);
            await _paymentRepository.SaveAsync();
            return Ok($"The Payment with ID {payment.Id} Deleted Successfully");
        }
    }
}
