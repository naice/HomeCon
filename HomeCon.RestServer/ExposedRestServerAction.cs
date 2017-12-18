using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace HomeCon.RestServer
{
    internal class ExposedRestServerAction
    {
        public Type InputType { get; set; }
        public Type OutputType { get; set; }
        public string Route { get; set; }
        public string[] Methods { get; set; }
        public ExposedRestServerService RestServerService { get { return _restServerService; } }

        private readonly ExposedRestServerService _restServerService;
        private readonly MethodInfo _methodInfo;

        public ExposedRestServerAction(ExposedRestServerService restServerService, MethodInfo methodInfo)
        {
            _restServerService = restServerService;
            _methodInfo = methodInfo;
        }

        public async Task<object> Execute(HttpListenerContext context, object param)
        {
            // VOID
            if (OutputType == null && InputType == null)
            {
                ExecuteVoid(context);
                return null;
            }
            if (OutputType == null)
            {
                ExecuteVoid(context, param);
                return null;
            }
            if (OutputType == typeof(Task) && InputType == null)
            {
                await ExecuteVoidAsync(context);
                return null;
            }
            if (OutputType == typeof(Task))
            {
                await ExecuteVoidAsync(context, param);
                return null;
            }

            // RESULT
            if (IsGenericTaskType(OutputType) && InputType == null)
            {
                return await ExecuteAsync(context);
            }
            if (IsGenericTaskType(OutputType))
            {
                return await ExecuteAsync(context, param);
            }
            if (InputType == null)
            {
                return ExecuteInternal(context);
            }

            return ExecuteInternal(context, param);
        }

        private object ExecuteInternal(HttpListenerContext context, object param)
        {
            return _methodInfo.Invoke(_restServerService.GetInstance(context), new object[] { param });
        }
        private object ExecuteInternal(HttpListenerContext context)
        {
            return _methodInfo.Invoke(_restServerService.GetInstance(context), new object[0]);
        }
        private async Task<object> ExecuteAsync(HttpListenerContext context, object param)
        {
            //return await (dynamic)_methodInfo.Invoke(_restServerService.GetInstance(context), new object[] { param });
            var task = (Task)_methodInfo.Invoke(_restServerService.GetInstance(context), new object[] { param });
            var result = await Task.Run<object>(() => { return task.GetType().GetProperty("Result").GetValue(task); });

            return result;
        }
        private async Task<object> ExecuteAsync(HttpListenerContext context)
        {
            // dynamic fails with void, dont know why.
            var task = (Task)_methodInfo.Invoke(_restServerService.GetInstance(context), new object[0]);
            var result = await Task.Run<object>(() => { return task.GetType().GetProperty("Result").GetValue(task); });

            return result;
        }
        private async Task ExecuteVoidAsync(HttpListenerContext context, object param)
        {
            await (Task)_methodInfo.Invoke(_restServerService.GetInstance(context), new object[] { param });
        }
        private async Task ExecuteVoidAsync(HttpListenerContext context)
        {
            await (Task)_methodInfo.Invoke(_restServerService.GetInstance(context), new object[0]);
        }
        private void ExecuteVoid(HttpListenerContext context, object param)
        {
            _methodInfo.Invoke(_restServerService.GetInstance(context), new object[] { param });
        }
        private void ExecuteVoid(HttpListenerContext context)
        {
            _methodInfo.Invoke(_restServerService.GetInstance(context), new object[0]);
        }

        private static bool IsGenericTaskType(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Task<>);
        }
    }
}