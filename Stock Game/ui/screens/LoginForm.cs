using System;
using System.Collections.Generic;
using System.IO;

using Stock_Game.core;

namespace Stock_Game.ui.screens
{
    public class LoginForm : FormScreen
    {
        List<Profile> loadedProfiles = new List<Profile>();
		string loginMessage = "Login successful. Loading stock data for your account...";
		
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
            if(!Directory.Exists(Profile.defaultProfileLocation))
				Directory.CreateDirectory(Profile.defaultProfileLocation);
			else{
				string[] files = Directory.GetFiles(Profile.defaultProfileLocation);
				foreach (string f in files)
					loadedProfiles.Add(new Profile(f));
			}
        }

        public override void EnterAction()
        {
            base.EnterAction();
			
			if(inputs[0].ValueText.Equals("") || inputs[1].ValueText.Equals("")){
				Launcher.stockGame.GoBack();
				return;
			}
			
            LoadProfiles();
            bool foundUser = false;
            foreach (Profile p in loadedProfiles)
            {
                if (p.Name.Equals(inputs[0].ValueText, StringComparison.InvariantCultureIgnoreCase))
                {
                    foundUser = true;
                    if (Cryptography.CheckHash(inputs[1].ValueText, p.HashedPassword))
                    {
                        StockGame.WriteNotification(loginMessage);
                        Launcher.stockGame.Account = p;
                        Launcher.stockGame.ChangeScreen(new MainMenu());
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