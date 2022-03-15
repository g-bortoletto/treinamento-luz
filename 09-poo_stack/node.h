#ifndef NODE_H
#define NODE_H


template<class T>
class Node
{
private:
	T m_data;
	Node<T>* m_next;

public:
	Node(T data, Node<T>* next = nullptr);
	~Node();

	void setData(const T data);
	T getData() const;

	void setNext(const Node<T>* next);
	Node<T>* getNext() const;
};


#endif