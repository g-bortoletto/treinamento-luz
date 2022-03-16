#ifndef LINK_H
#define LINK_H


class Link
{
private:
	int m_data;
	Link* m_next;

public:
	Link(int data);
	~Link();

	void setData(int data);
	void setNext(Link* next);

	int getData();
	Link* getNext();

	Link* operator=(Link* other);
};


#endif