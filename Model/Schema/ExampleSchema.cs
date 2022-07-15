using Newtonsoft.Json.Schema;

namespace PBTeste.Model.Schema
{
   public class ExampleSchema
    {
        public static JSchema ExampleJson()
        {
            JSchema schema = JSchema.Parse(@"{
    '$schema': 'http://json-schema.org/draft-07/schema',
    'type': 'object',
    'required': [
        'example'
    ],
    'properties': {
        'example': {
            'type': 'string'
        }
            }
}");
            return schema;

        }
    }
}
