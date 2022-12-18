class Token(object):
    def __init__(self, token, value = "") -> None:
        self.token = token
        self.value = value
        
    def __str__(self) -> str:
        return f"{self.token}:{self.value}"

class Node(object):
    def __init__(self, token):
        self.token = token
        self.children = []

class Solution(object):
    def __init__(self, str) -> None:
        self.LEFT_BRACKET = Token('LEFT_BRACKET')
        self.RIGHT_BRACKET = Token('LEFT_BRACKET')
        self.COMMA = Token('COMMA')
        self.ARRAY = Token('ARRAY')
        self.DIGIT = Token('DIGIT')
        self.INVALID = Token('INVALID')
        self.tokens = []
        self.node = None
        self.str = str

    def debug(self):
        for t in self.tokens:
            print(f'{t.token} with {t.value}')

        self.debugTree(self.node)

    def debugTree(self, node):
        if node != None:
            str = ""

            for c in node.children:
                if isinstance(c, Node):
                    self.debugTree(c)
                else:
                    str += f"{c} "
            print (str)

    def compare(self, tree) -> bool:
        return self.compareX(self.node, tree)

    def compareX(self, lhsTree, rhsTree):
        """
        If both values are integers, the lower integer should come first. 
        If the left integer is lower than the right integer, the inputs are in the right
        order. If the left integer is higher than the right integer, the inputs are not
        in the right order. Otherwise, the inputs are the same integer; continue checking
        the next part of the input.
        """
        result = len(lhsTree.children) <= len(rhsTree.children)
        childCount = max(len(lhsTree.children), len(rhsTree.children))

        for idx in range(childCount):
            lhChild = lhsTree.children[idx] if idx < len(lhsTree.children) else None
            rhChild = rhsTree.children[idx] if idx < len(rhsTree.children) else None

            if (lhChild == None and rhChild != None):
                # If the left list runs out of items first, it's in the right order
                return True
            
            if (rhChild == None and lhChild != None):
                # If the right list runs out of items before the left list, it's wrong
                return False
            
            if lhChild != None and not isinstance(lhChild, Node) and rhChild != None and not isinstance(rhChild, Node):
                isGood = lhChild <= rhChild
                if isGood:
                    return True
            else:
                if not isinstance(lhChild, Node):
                    tempValue = lhChild
                    lhChild = Node(Token(self.DIGIT, lhChild))
                    lhChild.children.append(tempValue)
                if not isinstance(rhChild, Node):
                    value = rhChild
                    rhChild = Node(Token(self.ARRAY))
                    rhChild.children.append(value)                

                if lhChild != None and rhChild != None:
                    result = result and self.compareX(lhChild, rhChild)                    

        return result

    def compareI(self, lhsTree, rhsTree):
        node = lhsTree
        otherNode = rhsTree
        result = len(node.children) <= len(otherNode.children)

        for idx in range(len(node.children)):
            child = node.children[idx]
            otherChild = otherNode.children[idx] if idx < len(otherNode.children) else None

            if not isinstance(child, Node) and not isinstance(otherChild, Node):
                if otherChild == None:
                    return result # There are no more RHS values
                else:
                    if child <= otherChild:
                        return result
            else:
                if not isinstance(child, Node):
                    tempValue = child
                    child = Node(Token(self.DIGIT, child))
                    child.children.append(tempValue)
                if not isinstance(otherChild, Node):
                    value = otherChild
                    otherChild = Node(Token(self.ARRAY))
                    otherChild.children.append(value)                

                result = result and self.compareI(child, otherChild)
                if result == False:
                    return False

        return result

    def eval(self):
        self.tokenize(self.str)
        self.parse()

    def tokenize(self, str):
        for ch in str:
            if ch == '[':
                self.tokens.append(self.LEFT_BRACKET)
            elif ch == ']':
                self.tokens.append(self.RIGHT_BRACKET)
            elif ch == ',':
                self.tokens.append(self.COMMA)
            elif ch >= '0' and ch <= '9':
                self.tokens.append(Token(self.DIGIT, int(ch)))
            else:
                self.tokens.append(self.INVALID)
        
    def parse(self):
        node = self.node
        digit = ""
        nodes = []
        if node != None:
            nodes.append(node)

        for p in self.tokens:
            if p == self.LEFT_BRACKET:
                if self.node == None:
                    self.node = Node(self.ARRAY)
                    node = self.node
                    nodes.append(node)
                else:
                    newNode = Node(self.ARRAY)
                    node.children.append(newNode)
                    node = newNode
                    nodes.append(node)
            elif p.token == self.DIGIT:
                digit += f"{p.value}"
            elif p == self.COMMA:
                if digit != "":
                    node.children.append(int(digit))
                digit = ""
            elif p == self.RIGHT_BRACKET:
                if digit != "":
                    node.children.append(int(digit))
                digit = ""
                nodes.pop()
                node = nodes[len(nodes) - 1] if len(nodes) > 0 else None
                pass

if (__name__ == "__main__"):
    input = open("input.txt", "r")
    lines = input.readlines()
    count = (int)(len(lines)/3)
    total = 0
    for n in range(count):
        str1 = lines[n * 3]
        str2 = lines[n * 3 + 1]

        s1 = Solution(str1)
        s1.eval()
    
        s2 = Solution(str2)
        s2.eval()
        isValid = s1.compare(s2.node)
        total = total + (n+1) if isValid else total
        print(f"Index #{n+1} : {isValid}")

    print(f"Total: {total}")
