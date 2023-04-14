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
        public HttpStatusCode StatusCode { get; private set; }

        public YException(string message, HttpStatusCode statusCode)
            : base($"{message} StatusCode={statusCode}")
        {
            StatusCode = statusCode;
        }

        public YException(AmazonS3Exception ex, string message, HttpStatusCode statusCode) 
            : base($"{message} StatusCode={statusCode}", ex) 
        {
            StatusCode = statusCode;
        }

        internal YException(AmazonS3Exception ex)
            : this(ex, $"{ex.Message} Code={ex.ErrorCode}", ex.StatusCode)
        { 
        }

        internal YException(AmazonS3Exception ex, string addMessage)
            : this(ex, $"{ex.Message} {addMessage}. Code={ex.ErrorCode}", ex.StatusCode)
        {
        }
    }
}
