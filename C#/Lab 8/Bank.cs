namespace nicorueda
{
    class Bank
    {
        // TODO: Need to add a ToString method and an Equals method to this class
        private string bankName;

        public string BankName
        {
            get
            {
                return bankName;
            }
        }

        public override bool Equals(object? obj)
        {
            bool result = true;

            Bank b = (Bank)obj;

            if ((this.bankName == b.bankName) && (this.bankAccounts.Count == b.bankAccounts.Count))
            {
                for (int i = 0; i < this.bankAccounts.Count; i++)
                {
                    if (!this.bankAccounts[i].Equals(b.bankAccounts[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else result = false;
            return result;

        }


        public override string ToString()
        {
            return $"Bank name: {bankName}. Number of accounts: {bankAccounts.Count}";
        }


        private List<Account> bankAccounts;

        static int newAccountNumber = 1;

        public Bank(string newBankName)
        {
            bankName = newBankName;
            bankAccounts = new List<Account>();
        }

        public Account AddAccount(string inName, string inAddress, decimal inBalance)
        {
            Account result = new Account(inName, inAddress, inBalance, newAccountNumber);
            bankAccounts.Add(result);
            newAccountNumber = newAccountNumber + 1;
            return result;
        }
        public Account FindAccount(int searchNumber)
        {
            foreach (Account a in bankAccounts)
            {
                if (a.AccountNumber == searchNumber)
                    return a;
            }
            return null;
        }

        public bool DeleteAccount(int deleteNumber)
        {
            Account del = FindAccount(deleteNumber);
            if (del != null)
            {
                bankAccounts.Remove(del);
                return true;
            }
            return false;
        }

        public void Save(System.IO.TextWriter textOut)
        {
            textOut.WriteLine(bankName);
            textOut.WriteLine(newAccountNumber);
            textOut.WriteLine(bankAccounts.Count);
            foreach (Account a in bankAccounts)
            {
                if (a != null)
                {
                    a.Save(textOut);
                }
            }
        }

        public void Save(string filename)
        {
            System.IO.TextWriter textOut = null;

            try
            {
                textOut = new System.IO.StreamWriter(filename);
                Save(textOut);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textOut != null) textOut.Close();
            }
        }

        public static Bank Load(System.IO.TextReader textIn)
        {
            string bankName = textIn.ReadLine();
            Bank result = new Bank(bankName);
            newAccountNumber = int.Parse(textIn.ReadLine());
            int numberOfAccounts = int.Parse(textIn.ReadLine());
            for (int i = 0; i < numberOfAccounts; i++)
            {
                Account a = Account.Load(textIn);
                result.bankAccounts.Add(a);
            }
            return result;
        }

        public static Bank Load(string filename)
        {
            Bank result;
            System.IO.TextReader textIn = null;
            try
            {
                textIn = new System.IO.StreamReader(filename);
                result = Load(textIn);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (textIn != null) textIn.Close();
            }
            return result;
        }
    }

}