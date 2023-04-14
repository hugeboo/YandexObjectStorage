using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dotkit.YandexObjectStorage.FileSystem
{
    public sealed class YException : Exception
    {
        public HttpStatusCode? StatusCode { get; set; }

        public YException(string message, HttpStatusCode? statusCode = null) 
            : base($"{message} StatusCode={statusCode}") 
        {
            StatusCode = statusCode;
        }

        internal YException(AmazonS3Exception ex)
            : this($"{ex.Message} Code={ex.ErrorCode}", ex.StatusCode)
        { 
        }

        internal YException(AmazonS3Exception ex, string addMessage)
            : this($"{ex.Message} {addMessage}. Code={ex.ErrorCode}", ex.StatusCode)
        {
        }
    }
}
