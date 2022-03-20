using SchoolApp.Models;

using System.Collections.ObjectModel;
using System.Windows.Input;
using System;

namespace SchoolApp.ViewModels
{
	internal class MainViewModel : ObservableObject
	{
		public ObservableCollection<Person> People { get; set; }

		public ICommand BtnCreate { get; private set; }
		public ICommand BtnRead { get; private set; }
		public ICommand BtnUpdate { get; private set; }
		public ICommand BtnDelete { get; private set; }

		public string TbFirstName { get; set; } = string.Empty;
		public string TbLastName { get; set; } = string.Empty;
		public DateTime DpBirthDay { get; set; } = DateTime.Now;

		public Person SelectedEntry { get; set; }

		public MainViewModel(ObservableCollection<Person> people = null)
		{
			People = people != null ? people : new ObservableCollection<Person>();

			BtnCreate = new DelegateCommand(CreateAction);

			BtnRead = new DelegateCommand(ReadAction);

			BtnUpdate = new DelegateCommand(UpdateAction);

			BtnDelete = new DelegateCommand(DeleteAction);
		}

		public void AddPerson(Person person)
		{
			if (person != null)
			{
				People.Add(person);
			}
		}

		public void CreateAction(object _)
		{
			if (!string.IsNullOrEmpty(TbFirstName) && !string.IsNullOrEmpty(TbLastName))
			{
				Person person = new(TbFirstName, TbLastName, DpBirthDay);
				ResetFields();
				AddPerson(person);
			}
		}

		public void ReadAction(object _)
		{
			if (SelectedEntry != null)
			{
				TbFirstName = SelectedEntry.FirstName;
				RaisePropertyChangedEvent(nameof(TbFirstName));
				TbLastName = SelectedEntry.LastName;
				RaisePropertyChangedEvent(nameof(TbLastName));
				DpBirthDay = SelectedEntry.BirthDay;
				RaisePropertyChangedEvent(nameof(DpBirthDay));
			}
		}

		public void UpdateAction(object _)
		{
			if (!string.IsNullOrEmpty(TbFirstName) && !string.IsNullOrEmpty(TbLastName))
			{
				Person person = new(TbFirstName, TbLastName, DpBirthDay);
				ResetFields();
				for (int i = 0; i < People.Count; ++i)
				{
					if (SelectedEntry.FirstName == People[i].FirstName &&
						SelectedEntry.LastName == People[i].LastName &&
						SelectedEntry.BirthDay == People[i].BirthDay)
					{
						//People[i].FirstName = person.FirstName;
						//People[i].LastName = person.LastName;
						//People[i].BirthDay = person.BirthDay;
						//break;
						People.RemoveAt(i);
						People.Insert(i, person);
					}
				}
			}
		}

		public void DeleteAction(object _)
		{
			People.Remove(SelectedEntry);
		}

		private void ResetFields()
		{
			TbFirstName = string.Empty;
			RaisePropertyChangedEvent(nameof(TbFirstName));
			TbLastName = string.Empty;
			RaisePropertyChangedEvent(nameof(TbLastName));
			DpBirthDay = DateTime.Now;
			RaisePropertyChangedEvent(nameof(DpBirthDay));
		}
	}
}
