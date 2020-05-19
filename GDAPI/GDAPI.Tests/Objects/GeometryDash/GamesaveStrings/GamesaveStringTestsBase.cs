using GDAPI.Objects.GeometryDash.GamesaveStrings;
using NUnit.Framework;
using System;
using System.IO;

namespace GDAPI.Tests.Objects.GeometryDash.GamesaveStrings
{
    public abstract class GamesaveStringTestsBase<T>
        where T : BaseGamesaveString
    {
        protected T Instance { get; private set; }
        protected T EncryptedInstance { get; private set; }
        protected T DecryptedInstance { get; private set; }

        protected abstract string FileType { get; }

        public GamesaveStringTestsBase()
        {
            EncryptedInstance = GetNewInstance(ReadEncryptedFile());
            DecryptedInstance = GetNewInstance(ReadDecryptedFile());
        }

        [Test]
        public void EncryptionState()
        {
            Assert.IsTrue(EncryptedInstance.IsEncrypted);
            Assert.IsFalse(EncryptedInstance.IsDecrypted);
            Assert.IsTrue(DecryptedInstance.IsDecrypted);
            Assert.IsFalse(DecryptedInstance.IsEncrypted);
        }
        [Test]
        public void Decrypt()
        {
            Instance = GetNewInstance(EncryptedInstance.RawString);

            Instance.TryDecrypt(out string result, false);
            AssertEncryptionState(result, DecryptedInstance, EncryptedInstance, Instance.IsEncrypted);

            Instance.TryDecrypt(out result, true);
            AssertEncryptionState(result, DecryptedInstance, DecryptedInstance, Instance.IsDecrypted);

            void AssertEncryptionState(string result, T expectedResultEncryptionInstance, T expectedEncryptionInstance, bool expectedEncryptionState)
            {
                Assert.AreEqual(expectedResultEncryptionInstance.RawString, result);
                Assert.AreEqual(expectedEncryptionInstance.RawString, Instance.RawString);
                Assert.IsTrue(expectedEncryptionState);
            }
        }
        [Test]
        public void Encrypt()
        {
            Instance = GetNewInstance(DecryptedInstance.RawString);

            Instance.TryEncrypt(out string result, false);
            var t = GetNewInstance(result);
            t.TryDecrypt(out string reversedEncrypted, true);
            AssertEncryptionState(reversedEncrypted, DecryptedInstance, DecryptedInstance, Instance.IsDecrypted);
        }

        protected abstract T GetNewInstance(string s);

        private void AssertEncryptionState(string result, T expectedResultEncryptionInstance, T expectedEncryptionInstance, bool expectedEncryptionState)
        {
            Assert.AreEqual(expectedResultEncryptionInstance.RawString, result);
            Assert.AreEqual(expectedEncryptionInstance.RawString, Instance.RawString);
            Assert.IsTrue(expectedEncryptionState);
        }

        private string ReadDecryptedFile() => ReadResourceFile("Decrypted");
        private string ReadEncryptedFile() => ReadResourceFile("Encrypted");
        private string ReadResourceFile(string encryptionState) => File.ReadAllText($@"{TestResourceContainer.BaseResourceStringsDirectory}\GamesaveCipher\{encryptionState} {FileType}.txt");
    }
}
