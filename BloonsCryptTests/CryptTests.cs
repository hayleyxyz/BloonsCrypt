namespace BloonsCryptTests
{
    [TestClass]
    public class CryptTests
    {
        [TestMethod]
        [DataRow(0x11223344, 0x15f33004u)]
        [DataRow(0x44332211, 0xfce31dd9u)]
        public void Calculates_Checksum(int seed, uint expectedChecksum)
        {
            var data = new byte[0x5000];

            (new Random(seed)).NextBytes(data);

            var crc = BloonsCrypt.Crypt.Checksum(data, 0, data.Length);

            Assert.AreEqual(expectedChecksum, crc);

        }
    }
}