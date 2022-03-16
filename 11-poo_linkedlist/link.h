#ifndef LINK_H
#define LINK_H


template<class T>
class Link
{
private:
	T m_data;
	Link<T>* m_next;

public:
	Link(T data);
	~Link();

	void setData(T data);
	void setNext(Link<T>* next);

	T getData();
	Link<T>* getNext();

	Link<T>* operator=(Link<T>* other);
};


#endif