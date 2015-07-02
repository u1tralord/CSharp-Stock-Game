using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    public class CreateAccount : FormScreen
    {
        List<Profile> loadedProfiles = new List<Profile>();

        public CreateAccount()
        {
            title = "Create Account";
            inputs = new List<FormInput>();

            FormInput username = new FormInput("Username", 15, 0, 0);
            username.Highlighted = true;
            inputs.Add(username);

            FormInput password = new FormInput("Password", 15, 0, 0);
            inputs.Add(password);

            FormInput password2 = new FormInput("Confirm Pass", 15, 0, 0);
            inputs.Add(password2);

            FormInput startingAmount = new FormInput("Beginning Balance", 15, 0, 0);
            startingAmount.ValueText = "10000";
            inputs.Add(startingAmount);

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
                if (p.Name.Equals(inputs[0].ValueText, StringComparison.InvariantCultureIgnoreCase))
                    foundUser = true;

            if (!foundUser)
            {
                if (inputs[1].ValueText.Equals(inputs[2].ValueText))
                {
                    Profile profile = new Profile(inputs[0].ValueText, Cryptography.GetHash(inputs[2].ValueText), Convert.ToInt32(inputs[3].ValueText));
                    profile.Save();
                    StockGame.Account = profile;
                    StockGame.ChangeScreen(new MainMenu(), this);
                }
            }
        }
    }
}