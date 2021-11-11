using passwordHasher;

var pwdHasher = new PasswordHasher();

var output = pwdHasher.HashPassword("@joHnDow");

Console.WriteLine(output);
