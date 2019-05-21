using BuilderUtils.Models;
using Newtonsoft.Json.Linq;

namespace BuilderUtils.Services
{
    public interface IBlipBuilderFlowFactory
    {
        BlipBuilderFlow Build(JObject deserializedJsonFlow);
    }
}
