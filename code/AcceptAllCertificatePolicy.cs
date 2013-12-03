using System;

namespace GU.Net
{
	/// <summary>
	/// Certificate Policy that accepts any X509 Certificate when 
	/// making an HTTPS (SSL) request to a remote web server.  Only 
	/// use this for testing or internal requests.
	/// </summary>
	/// <remarks>
	/// Before you make a Web Request, you'll need to set the Certificate Policy 
	/// to accept all certificates using the following line.
	/// 
	/// ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
	/// </remarks>
	public class AcceptAllCertificatePolicy : System.Net.ICertificatePolicy
	{
		#region ICertificatePolicy Members

		public bool CheckValidationResult(System.Net.ServicePoint srvPoint, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Net.WebRequest request, int certificateProblem)
		{
			return true; //Always accepts.
		}

		#endregion
	}
}
