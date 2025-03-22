class Main {
    public static void main(String[] args) {
        BST tree = new BST();
        tree.root = tree.insert(tree.root, 45);
        tree.root = tree.insert(tree.root, 15);
        tree.root = tree.insert(tree.root, 79);
        tree.root = tree.insert(tree.root, 90);
        tree.root = tree.insert(tree.root, 10);
        tree.root = tree.insert(tree.root, 55);
        tree.root = tree.insert(tree.root, 12);
        tree.root = tree.insert(tree.root, 20);
        tree.root = tree.insert(tree.root, 50);

        System.out.println("Binary Search Tree Structure:");
        tree.displayTree(tree.root, "", true);
    }
}

class Node {
    int data;
    Node left, right;

    public Node(int item) {
        data = item;
        left = right = null;
    }
}

class BST {
    Node root;

    BST() {
        root = null;
    }

    Node findMinimum(Node cur) {
        while (cur.left != null) {
            cur = cur.left;
        }
        return cur;
    }

    Node insert(Node root, int item) {
        if (root == null)
            return new Node(item);
        if (item < root.data)
            root.left = insert(root.left, item);
        else
            root.right = insert(root.right, item);
        return root;
    }

    boolean search(Node root, int item) {
        if (root == null)
            return false;
        if (root.data == item)
            return true;
        if (item < root.data)
            return search(root.left, item);
        return search(root.right, item);
    }

    Node delete(Node root, int item) {
        if (root == null)
            return root;
        if (item < root.data)
            root.left = delete(root.left, item);
        else if (item > root.data)
            root.right = delete(root.right, item);
        else {
            if (root.left == null)
                return root.right;
            else if (root.right == null)
                return root.left;
            Node temp = findMinimum(root.right);
            root.data = temp.data;
            root.right = delete(root.right, temp.data);
        }
        return root;
    }

    void displayTree(Node root, String indent, boolean isLeft) {
        if (root != null) {
            System.out.println(indent + (isLeft ? "L-- " : "R-- ") + root.data);
            displayTree(root.left, indent + "   ", true);
            displayTree(root.right, indent + "   ", false);
        }
    }
}
