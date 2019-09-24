using System.Text;

namespace Profiling
{
    class Generator
    {
        public static string GenerateDeclarations()
        {
            var result = new StringBuilder();
            var className = "class C";
            var structName = "struct S";
            var byteName = "byte Value";
            foreach (var i in Constants.FieldCounts)
            {
                var byteBuilder = new StringBuilder();
                for (int j = 0; j < i; ++j)
                {
                    byteBuilder.Append(byteName + j + "; ");
                }
                var byteString = byteBuilder.ToString();
                result.Append(structName + i + "{" + byteString + "}");
                result.Append(className + i + "{" + byteString + "}");
            }
            return result.ToString();
        }

        public static string GenerateArrayRunner()
        {
            var result = new StringBuilder("public class ArrayRunner : IRunner {");
            var tempPart = new StringBuilder("public void Call(bool isClass, int size, int count) {");
            foreach (var i in Constants.FieldCounts)
            {
                result.Append(string.Format(@"void PC{0}() {{ var array = new C{0}[Constants.ArraySize]; 
                        for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C{0}();}} void PS{0}
                        () {{ var array = new S{0}[Constants.ArraySize];}}", i));
                tempPart.Append(string.Format(@"if (isClass && size == {0}) {{ for (int i = 0; i < count; i++) PC{0}
                        (); return;}} if (!isClass && size == {0}) {{ for(int i = 0; i < count; i++) PS{0}
                        (); return;}}", i));
            }
            result.Append(tempPart.ToString() + " throw new ArgumentException();}}");
            return result.ToString();
        }

        public static string GenerateCallRunner()
        {
            var result = new StringBuilder("public class CallRunner : IRunner {");
            var tempPart = new StringBuilder("public void Call(bool isClass, int size, int count){");
            foreach (var i in Constants.FieldCounts)
            {
                result.Append(string.Format("void PC{0}(C{0} o) {{ }} ", i));
                result.Append(string.Format("void PS{0}(S{0} o) {{ }} ", i));
                tempPart.Append(string.Format(@"if (isClass && size == {0}) 
                    {{ var o = new C{0}(); for (int i = 0; i < count; i++) PC{0}(o); return; }} ", i));
                tempPart.Append(string.Format(@"if (!isClass && size == {0}) 
                    {{ var o = new S{0}(); for (int i = 0; i < count; i++) PS{0}(o); return; }} ", i));
            }
            result.Append(tempPart.ToString() + " throw new ArgumentException();}}");
            return result.ToString();
        }
    }
}
