using AutoMapper;
using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastScooter.API.Controllers;


[Route("api/v1/payment")]
[ApiController]

public class PaymentController: ControllerBase
{
    private IPaymentDomain _paymentDomain;
    private IMapper _mapper;

    public PaymentController(IPaymentDomain paymentDomain, IMapper mapper)
    {
        _paymentDomain = paymentDomain;
        _mapper = mapper;
    }
    
    [HttpPost(Name = "PostPayment")]
    public bool Post([FromBody] PaymentRequest paymentRequest)
    {
        var payment = _mapper.Map<PaymentRequest, Payment>(paymentRequest);
        payment.PaymentMethod = "CreaditCard";
        return _paymentDomain.CreateNewPayment(payment);
    }
    
    [HttpGet("userId/{userId}", Name = "GetAllPaymentByUserId")]
    public async Task<ActionResult<List<PaymentResponse>>> GetAllByUserId(int userId)
    {
        try
        {
            var payments = await _paymentDomain.GetAllByUserId(userId);

            // Mapea la lista de favoritos al tipo de respuesta esperado
            var paymentResponses = _mapper.Map<List<PaymentResponse>>(payments);

            return Ok(paymentResponses);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    
    [HttpGet("renId/{userId}", Name = "GetAllPaymentByRentId")]
    public async Task<ActionResult<List<PaymentResponse>>> GetAllByRentId(int userId)
    {
        try
        {
            var payments = await _paymentDomain.GetAllByRentId(userId);

            // Mapea la lista de favoritos al tipo de respuesta esperado
            var paymentResponses = _mapper.Map<List<PaymentResponse>>(payments);

            return Ok(paymentResponses);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

}