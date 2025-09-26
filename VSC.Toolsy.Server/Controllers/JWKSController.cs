using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using VSC.Toolsy.Repositories.Data;

namespace VSC.Toolsy.Server.Controllers
{
    [Route(".well-known")]
    [ApiController]
    public class JWKSController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public JWKSController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("jwks.json")]
        public IActionResult GetJWKS()
        {
            var keys = _context.SigningKeys.Where(k => k.IsActive).ToList();
            var jwks = new
            {
                //kty, use, kid, alg, n, e
                keys = keys.Select(k => new
                {
                    kty = "RSA",    // Key type (RSA)
                    use = "sig",    // Usage (sig for signature)
                    kid = k.KeyId,  // Key ID to identify the key
                    alg = "RS256",  // Algorithm (RS256 for RSA SHA-
                    n = Base64UrlEncoder.Encode(GetModulus(k.PublicKey)), // Modulus (Base64URL-encoded)
                    e = Base64UrlEncoder.Encode(GetExponent(k.PublicKey)) // Exponent (Base64URL-encoded)
                })
            };
            return Ok(jwks);
        }
        private byte[] GetModulus(string publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            var parameters = rsa.ExportParameters(false);
            rsa.Dispose();
            if (parameters.Modulus == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }
            return parameters.Modulus;
        }
        private byte[] GetExponent(string publicKey)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
            var parameters = rsa.ExportParameters(false);
            rsa.Dispose();
            if (parameters.Exponent == null)
            {
                throw new InvalidOperationException("RSA parameters are not valid.");
            }
            return parameters.Exponent;
        }
    }
}
