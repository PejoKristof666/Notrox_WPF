using Notrox.Model;

namespace Notrox_WPF_Test
{
    [TestClass]
    public sealed class Test1
    {
        private UsersClass u;

        [TestInitialize]
        public void init()
        {
            
        }

        [TestMethod]
        public void TestMethod1()
        {
            u = new UsersClass();
        }

    }
}
