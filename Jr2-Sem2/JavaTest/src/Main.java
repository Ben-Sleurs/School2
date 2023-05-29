import test.pkg.Test;
import java.util.*;
import java.util.stream.Collectors;


// Press Shift twice to open the Search Everywhere dialog and type `show whitespaces`,
// then press Enter. You can now see whitespace characters in your code.
public class Main {
    public static void main(String[] args) {
            Node fourteen = new Node(14);
            Node eleven = new Node(11);
            Node twelve = new Node(12);
            Node five = new Node(5);
            Node nine = new Node(9);
            Node twentyfive = new Node(25);
            Node twenty = new Node(20);
            twenty.left = nine; twenty.right = twentyfive;
            nine.parent = twenty; nine.left = five; nine.right = twelve;
            twentyfive.parent = twenty;
            five.parent = nine;
            twelve.parent = nine; twelve.left = eleven; twelve.right = fourteen;
            eleven.parent = twelve;
            fourteen.parent = twelve;

        var searchTree = new BinarySearchTree();
        Integer result = searchTree.findInOrderSuccessor(twentyfive);
        System.out.println(result);

        }

    static class BinarySearchTree {
        Node root;
        Integer findInOrderSuccessor(Node inputNode){
            Integer parent = lowestDifferenceParent(inputNode, inputNode.key);
            Integer child = lowestDifferenceDown(inputNode, inputNode.key);

            List<Integer> values = new ArrayList<>();
            values.add(child);
            values.add(parent);
            values = values.stream().filter(Objects::nonNull).collect(Collectors.toList());
            values = values.stream().filter(x -> x >0).collect(Collectors.toList());
            var a = values.stream().min(Integer::compareTo).orElse(0);
            return a;

        }
        Integer lowestDifferenceDown(Node inputNode, Integer startValue){
            if(inputNode == null){
                return null;
            }
            Integer left = lowestDifferenceDown(inputNode.left, startValue);
            Integer right = lowestDifferenceDown(inputNode.right, startValue);
            if(left == null || right == null){
                return inputNode.key - startValue;
            }
            Map<Node,Integer> dictionary = new HashMap<>();
            dictionary.put(inputNode, inputNode.key - startValue);
            dictionary.put(inputNode.left, left);
            dictionary.put(inputNode.right, right);
            return dictionary.entrySet().stream()
                    .filter(Objects::nonNull)
                    .filter(x -> x.getValue() > 0)
                    .collect(Collectors.toMap(Map.Entry::getKey,Map.Entry::getValue))
                    .entrySet().stream().min(Map.Entry.comparingByValue()).map(Map.Entry::getValue).orElse(null);
        }
        Integer lowestDifferenceParent(Node inputNode, Integer startValue){
            if(inputNode.parent == null){
                return inputNode.key - startValue;
            }
            Node child = inputNode.parent.left == inputNode ? inputNode.parent.right : inputNode.parent.left;
            Node parent = inputNode.parent;

            Integer childValue = lowestDifferenceDown(child,startValue);
            Integer parentValue = lowestDifferenceParent(parent,startValue);


            List<Integer> values = new ArrayList<>();
            values.add(childValue);
            values.add(parentValue);
            values = values.stream().filter(Objects::nonNull).collect(Collectors.toList());
            values = values.stream().filter(x -> x >0).collect(Collectors.toList());
            return values.stream().min(Integer::compareTo).orElse(null);

        }
    }
    static class Node {
        Integer key;
        Node left;
        Node right;
        Node parent;

        Node(Integer key) {
            this.key = key;
            left = null;
            right = null;
            parent = null;
        }
    }
    }