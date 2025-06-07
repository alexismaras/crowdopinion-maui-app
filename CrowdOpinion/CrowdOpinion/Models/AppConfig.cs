using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdOpinion.Models
{
    public static class AppConfig
    {
        public const string SUPABASE_URL = "https://kecjwzwulktcrykauczv.supabase.co"; // Your Supabase URL
        public const string SUPABASE_KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImtlY2p3end1bGt0Y3J5a2F1Y3p2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDkwNDg4ODEsImV4cCI6MjA2NDYyNDg4MX0.cMJj9tZmsuAMUc1emFG8eEkePIwCIo_1jsSDw6-AKxc"; // Your Supabase Key
        public const string SUPABASE_STORAGE_HANDLE = SUPABASE_URL + "/storage/v1/object/public/images/";
    }
}
