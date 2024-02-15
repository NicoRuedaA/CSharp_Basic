namespace nicorueda
{
    class Account
    {


        private string name;
        private string address;
        private decimal balance;
        private int accountNumber;

        public int AccountNumber
        {
            get
            {
                return accountNumber;
            }
        }

        public override bool Equals(object? obj)
        {
            Account p = (Account)obj;

            return (
                //this.accountNumber == p.accountNumber && 
                this.address == p.address && this.balance == p.balance && this.name == p.name);
        }

        public override string ToString()
        {
            return $"Account name: {name}, adress: {address}, balance {balance}, account number {accountNumber}";
        }

        public bool Save(System.IO.TextWriter textOut)
        {
            try
            {
                textOut.WriteLine(accountNumber);
                textOut.WriteLine(name);
                textOut.WriteLine(address);
                textOut.WriteLine(balance);
            }
            catch
            {
                return false;
            }
            return true;
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

        public static Account Load(System.IO.TextReader textIn)
        {
            int accountNumber = int.Parse(textIn.ReadLine());
            string nameText = textIn.ReadLine();
            string addressText = textIn.ReadLine();
            string balanceText = textIn.ReadLine();
            decimal balanceValue = decimal.Parse(balanceText);
            return new Account(nameText, addressText, balanceValue, accountNumber);
        }

        public static Account Load(string filename)
        {
            Account result;
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

        public Account(string inName, string inAddress, decimal inBalance, int inAccountNumber)
        {
            name = inName;
            address = inAddress;
            balance = inBalance;
            accountNumber = inAccountNumber;
        }

    }
}
