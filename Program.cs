using passwordHasher;

var pwdHasher = new PasswordHasher();

var output = pwdHasher.HashPassword("@joHnDow");

Console.WriteLine(output);

var verify = pwdHasher.VerifyPassword("@joHnDow", output);
Console.WriteLine(verify);
