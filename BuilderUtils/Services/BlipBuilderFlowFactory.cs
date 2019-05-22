using BuilderUtils.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuilderUtils.Services
{
    public class BlipBuilderFlowFactory : IBlipBuilderFlowFactory
    {
        public BlipBuilderFlow Build(JObject deserializedJsonFlow)
        {
            var flow = new BlipBuilderFlow();
            foreach (var box in deserializedJsonFlow)
            {
                var obj = new BoxProxy();
                obj.Key = box.Key;
                obj.Content = JsonConvert.DeserializeObject<BoxContent>(JsonConvert.SerializeObject(box.Value));
                flow.Proxy.Add(obj);
            }
            foreach (var item in flow.Proxy)
            {
                var box = new Box(item);
                flow.Boxes.Add(box);
            }
            return flow;
        }
    }
}
