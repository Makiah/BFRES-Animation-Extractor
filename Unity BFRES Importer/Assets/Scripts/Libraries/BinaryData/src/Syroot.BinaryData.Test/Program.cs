using System.IO;

namespace Syroot.BinaryData.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestObject obj = new TestObject() { X = TestStruct.Field2 };

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryDataWriter writer = new BinaryDataWriter(stream, true))
                {
                    writer.WriteObject(obj);
                }
                
                stream.Position = 0;
                using (BinaryDataReader reader = new BinaryDataReader(stream, true))
                {
                    obj = reader.ReadObject<TestObject>();
                }
            }
        }
    }

    class TestObjectBase
    {
        public int w;
    }
    
    class TestObject
    {
        public TestStruct X { get; set; }
    }

    enum TestStruct
    {
        Field1 = 0,
        Field2 = 1
    }
}