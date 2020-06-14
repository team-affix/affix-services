using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class UserCredentials : Form
    {
        public static string color1 { get; set; }
        public static string peerusername { get; set; }
        public static string IDKey { get; set; }
        public static string confirmationcode { get; set; }
        public static string serverpublickey = "<RSAKeyValue><Modulus>xMol4n5j960eUVcFx+eiH5Gdx/2cYPf1b4XpCuR9m10NI4kOdDoTBCJ8JQ4J7EnwIhxBFqk4K0OAQe8EtQJSpynIgxoyxFxKvdVvfa1e7r72jHTRhT6B0vuudrqnQjUc1+l99hD11qHfhZyLyrwWN/SoPmVl7B5yliKKwWEJNYy408Wm4jSLmsy7FXhJtgkKXunY90K7A5jLtH/uNbZmHYEgiikLWGNyvth962oCppRZyPq+hMDLafqgNAikdmZKFpbj+mlZAiG5Tw4Blze9Z/YeCne1D/e7gk6Iw7JJ9NoM3DpXmaM8HVWVQNVmByG1cWIwBHsOHB6ObMF15LYnM1tq/KYcVHc2toZUZRHd/WcGwyq7V3RsyQSWU72LXRGvJZQ1+s0BLt4WCD8oQrZeiV+3zfHHaGik7Tolumq8Z1l5c6dMuTpmrc6F7VQ+r0kQPhNamEo8kpkqai3tqKH1WP0oPInqTz3Hsof8xH5NMkZhlbd/iFjKeNrfdqMOYEbAZ3pODCoKOL7QOpAXc05bkN6ynmlvTW5b4nQA1CIqNvvsNKclbP5YyN2sFOUWnDb5+bbGBfqNorxRbyJlVwAykG3IU3hlIgn8orQMPTVBkyryRGnf5TNwe4lZQyExSIYh+n+Cw71hUeNGsdzjuXx7RHX08Ixaix7Gi769UXH950U=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string mypublickey { get; set; }
        public static string myprivatekey { get; set; }
        //public static List<object> credentials { get; set; }
        public static bool credentialsimported { get; set; }
        public static string verificationKey { get; set; }
        public static bool signedin { get; set; }
        public static string username { get; set; }
        public static string dhkey { get; set; }
        public static string authkey = "affixcenter";
        public static string LFA { get; set; }
        public static bool filesEncrypted { get; set; }
        public static string protocolmode = "tcp";
        public static List<object> IFA2details = new List<object> { };
        public static List<object> creds = new List<object> { };
        public static Form startform { get; set; }
        public UserCredentials()
        {
            InitializeComponent();
        }

        private void UserCredentials_Load(object sender, EventArgs e)
        {

        }
    }
}
