using System;

class MenuOption{
	
	public event EventHandler OptionSelected;
	
	public MenuOption(string text){
			this.OptionText = text;
	}
	
	public string OptionText{
		get;
		set;
	}
	
	public bool Highlighted{
		get;
		set;
	}
	
	public void Select(EventArgs e)
	{
		EventHandler handler = OptionSelected;
		if (handler != null)
		{
			handler(this, e);
		}
	}
}