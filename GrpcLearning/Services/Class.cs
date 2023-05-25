using DataAccess;

using Grpc.Core;

namespace GrpcLearning.Services
{
	public class ReaderService : Reader.ReaderBase
	{
		//протестировать
		public override Task<GetDataFromTextResponse> GetDataFromText(GetDataFromTextRequest request, ServerCallContext context)
		{
			var resultRext = File.ReadAllText(request.Path);
			var response = new GetDataFromTextResponse(){ Content = resultRext };
			return Task.FromResult(response);
		}
	}
}
