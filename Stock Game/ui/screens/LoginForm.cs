using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    public class LoginForm : FormScreen
    {
        List<Profile> loadedProfiles = new List<Profile>();

        public LoginForm()
        {
            title = "Login";
            inputs = new List<FormInput>();

            FormInput username = new FormInput("Username", 10, 0, 0);
            username.Highlighted = true;
            inputs.Add(username);

            FormInput password = new FormInput("Password", 15, 0, 0);
            inputs.Add(password);

            CalculateWindowSize();
            CalculateWindowPosition();

            textXPos = menuXPos + 2;
        }

        private void LoadProfiles()
        {
            string[] files = Directory.GetFiles(Profile.defaultProfileLocation);
            foreach (string f in files)
                loadedProfiles.Add(new Profile(f));
        }

        public override void EnterAction()
        {
            base.EnterAction();
            LoadProfiles();
            bool foundUser = false;
            foreach (Profile p in loadedProfiles)
            {
                if (p.Name.Equals(inputs[0].ValueText, StringComparison.InvariantCultureIgnoreCase))
                {
                    foundUser = true;
                    if (Cryptography.CheckHash(inputs[1].ValueText, p.HashedPassword))
                    {
                        errorString = "Correct Password";
                        StockGame.Account = p;
                        StockGame.ChangeScreen(new MainMenu(), this);
                    }
                    else
                    {
                        inputs[0].OptionText = "";
                        inputs[1].OptionText = "";
                        errorString = "Incorrect Password";
                    }
                }
            }
            if (!foundUser)
                errorString = "User Not Found " + inputs[0].ValueText;
        }
    }
}