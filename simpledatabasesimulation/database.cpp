#include <iomanip>
#include <iostream>
#include <map>
#include <set>
#include <sstream>
#include <stdexcept>
#include <string>
#include <vector>

using namespace std;

class Date {
public:
  // ����������� ����������� ����������, ���� ��� ��������� �����������
  Date(int new_year, int new_month, int new_day) {
    year = new_year;
    if (new_month > 12 || new_month < 1) {
      throw logic_error("Month value is invalid: " + to_string(new_month));
    }
    month = new_month;
    if (new_day > 31 || new_day < 1) {
      throw logic_error("Day value is invalid: " + to_string(new_day));
    }
    day = new_day;
  }

  int GetYear() const {
    return year;
  }
  int GetMonth() const {
    return month;
  }
  int GetDay() const {
    return day;
  }

private:
  int year;
  int month;
  int day;
};

// ���������� ��������� ��� ��� ���������� ��� ������������� �� � �������� ������ �������
bool operator<(const Date& lhs, const Date& rhs) {
  // ������������� ��� ������, ��� ������� ��� ����� ���������� �� <:
  // �������� ������ �� ����, ������ � ��� ��� ������ ���� � ������� ��
  return vector<int>{lhs.GetYear(), lhs.GetMonth(), lhs.GetDay()} <
      vector<int>{rhs.GetYear(), rhs.GetMonth(), rhs.GetDay()};
}

// ���� ����� �� ��������� ���������� � ������ �������
ostream& operator<<(ostream& stream, const Date& date) {
  stream << setw(4) << setfill('0') << date.GetYear() <<
      "-" << setw(2) << setfill('0') << date.GetMonth() <<
      "-" << setw(2) << setfill('0') << date.GetDay();
  return stream;
}

class Database {
public:
  void AddEvent(const Date& date, const string& event) {
    storage[date].insert(event);
  }

  bool DeleteEvent(const Date& date, const string& event) {
    if (storage.count(date) > 0 && storage[date].count(event) > 0) {
      storage[date].erase(event);
      return true;
    }
    return false;
  }

  int DeleteDate(const Date& date) {
    if (storage.count(date) == 0) {
      return 0;
    } else {
      const int event_count = storage[date].size();
      storage.erase(date);
      return event_count;
    }
  }

  set<string> Find(const Date& date) const {
    if (storage.count(date) > 0) {
      return storage.at(date);
    } else {
      return {};
    }
  }

  void Print() const {
    for (const auto& item : storage) {
      for (const string& event : item.second) {
        cout << item.first << " " << event << endl;
      }
    }
  }

private:
  map<Date, set<string>> storage;
};

Date ParseDate(const string& date) {
  istringstream date_stream(date);
  bool ok = true;

  int year;
  ok = ok && (date_stream >> year);
  ok = ok && (date_stream.peek() == '-');
  date_stream.ignore(1);

  int month;
  ok = ok && (date_stream >> month);
  ok = ok && (date_stream.peek() == '-');
  date_stream.ignore(1);

  int day;
  ok = ok && (date_stream >> day);
  ok = ok && date_stream.eof();

  if (!ok) {
    throw logic_error("Wrong date format: " + date);
  }
  return Date(year, month, day);
}

int main() {
  try {
    Database db;

    string command_line;
	cout << "Welcome to my simple Database simulator. You can create your events and manage them using"; 
	cout << " the following commands: add, delete and find events on a given day" << endl;
	cout << "When entering Date, please follow the following format - YYYY-MM-DD" << endl;
	cout << "Please, type your command: ";
    while (getline(cin, command_line)) {
      stringstream ss(command_line);

      string command;
      ss >> command;
      for (int i = 0; i < command.size(); i++)
      	command[i] = tolower(command[i]);

      if (command == "add") {

        string date_str, event;
        ss >> date_str >> event;
        const Date date = ParseDate(date_str);
        db.AddEvent(date, event);
		cout << "New Event was successfully Added" << endl;
		cout << "Please Type one of the following commands: Add, Delete, Find or Print: " << endl;
      } else if (command == "delete") {

        string date_str;
        ss >> date_str;
        string event;
        if (!ss.eof()) {
          ss >> event;
        }
        const Date date = ParseDate(date_str);
        if (event.empty()) {
          const int count = db.DeleteDate(date);
          cout << "Deleted " << count << " events" << endl;
        } else {
          if (db.DeleteEvent(date, event)) {
            cout << "Deleted successfully" << endl;
          } else {
            cout << "Event not found" << endl;
          }
        }

      } else if (command == "find") {
string date_str;
        ss >> date_str;
        const Date date = ParseDate(date_str);
        string ans = "";
        for (const string& event : db.Find(date)) {
			if (ans != "")
				ans += ", ";
			ans += event;
        }
		if (ans == "")
			cout << "There are no event on the given day" << endl;
		else 
			cout << "There are following events on the day " << date << ":" << ans << endl;
      } else if (command == "print") {

        db.Print();

      } else if (!command.empty()) {

        throw logic_error("Unknown command: " + command);

      }
    }
  } catch (const exception& e) {
    cout << e.what() << endl;
  }

  return 0;
}
