#ifndef STACK_H
#define STACK_H


template<class T>
class Stack
{
private:
	Node<T>* m_top;
	unsigned m_size;

public:
	Stack();
	~Stack();

	void setTop(const Node<T>* top);
	Node<T>* getTop() const;

	void setSize(const unsigned size);
	unsigned getSize() const;

	bool push(Node<T>* node);
	bool pop();

	void print();
};


#endif