// ***********************************************************************
// Assembly         : dotNetTips.Utility.Standard.Amazon
// Author           : David McCarter
// Created          : 06-26-2018
//
// Last Modified By : David McCarter
// Last Modified On : 06-26-2018
// ***********************************************************************
// <copyright file="AWSEnvironmentKeys.cs" company="McCarter Consulting - dotNetTips.com">
//     David McCarter - dotNetTips.com
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace dotNetTips.Utility.Standard.Amazon
{
    /// <summary>
    /// Enum AWSEnvironmentKeys
    /// </summary>
    public enum AWSEnvironmentKeys
    {
        /// <summary>
        /// The aws xray daemon address
        /// </summary>
        _AWS_XRAY_DAEMON_ADDRESS,
        /// <summary>
        /// The aws xray daemon port
        /// </summary>
        _AWS_XRAY_DAEMON_PORT,
        /// <summary>
        /// The handler
        /// </summary>
        _HANDLER,
        /// <summary>
        /// The lambda console socket
        /// </summary>
        _LAMBDA_CONSOLE_SOCKET,
        /// <summary>
        /// The lambda control socket
        /// </summary>
        _LAMBDA_CONTROL_SOCKET,
        /// <summary>
        /// The lambda log fd
        /// </summary>
        _LAMBDA_LOG_FD,
        /// <summary>
        /// The lambda runtime load time
        /// </summary>
        _LAMBDA_RUNTIME_LOAD_TIME,
        /// <summary>
        /// The lambda sb identifier
        /// </summary>
        _LAMBDA_SB_ID,
        /// <summary>
        /// The lambda shared memory fd
        /// </summary>
        _LAMBDA_SHARED_MEM_FD,
        /// <summary>
        /// The x amzn trace identifier
        /// </summary>
        _X_AMZN_TRACE_ID,
        /// <summary>
        /// The aws access key identifier
        /// </summary>
        AWS_ACCESS_KEY_ID,
        /// <summary>
        /// The aws default region
        /// </summary>
        AWS_DEFAULT_REGION,
        /// <summary>
        /// The aws execution env
        /// </summary>
        AWS_EXECUTION_ENV,
        /// <summary>
        /// The aws lambda function memory size
        /// </summary>
        AWS_LAMBDA_FUNCTION_MEMORY_SIZE,
        /// <summary>
        /// The aws lambda function name
        /// </summary>
        AWS_LAMBDA_FUNCTION_NAME,
        /// <summary>
        /// The aws lambda function version
        /// </summary>
        AWS_LAMBDA_FUNCTION_VERSION,
        /// <summary>
        /// The aws lambda log group name
        /// </summary>
        AWS_LAMBDA_LOG_GROUP_NAME,
        /// <summary>
        /// The aws lambda log stream name
        /// </summary>
        AWS_LAMBDA_LOG_STREAM_NAME,
        /// <summary>
        /// The aws region
        /// </summary>
        AWS_REGION,
        /// <summary>
        /// The aws secret access key
        /// </summary>
        AWS_SECRET_ACCESS_KEY,
        /// <summary>
        /// The aws session token
        /// </summary>
        AWS_SESSION_TOKEN,
        /// <summary>
        /// The aws xray context missing
        /// </summary>
        AWS_XRAY_CONTEXT_MISSING,
        /// <summary>
        /// The aws xray daemon address
        /// </summary>
        AWS_XRAY_DAEMON_ADDRESS,
        /// <summary>
        /// The aws s3 error bucket
        /// </summary>
        AWSS3ErrorBucket,
        /// <summary>
        /// The lambda runtime dir
        /// </summary>
        LAMBDA_RUNTIME_DIR,
        /// <summary>
        /// The lambda task root
        /// </summary>
        LAMBDA_TASK_ROOT,
        /// <summary>
        /// The language
        /// </summary>
        LANG,
        /// <summary>
        /// The ld library path
        /// </summary>
        LD_LIBRARY_PATH,
        /// <summary>
        /// The path
        /// </summary>
        PATH,
        /// <summary>
        /// The tz
        /// </summary>
        TZ
    }
}
