import { stringToChars } from "./utils";


// [token, score, index]
type OutputNode = [string[], number, number];

class TrieNode {
  public parent: TrieNode|null;
  public end: boolean;
  public children: {[firstSymbol: string]: TrieNode};
  public word: OutputNode;

  constructor() {
    this.parent = null;
    this.children = {};
    this.end = false;
    this.word = [[], 0, 0];
  }
}

export class Trie {
  public root: TrieNode;

  constructor() {
    this.root = new TrieNode();
  }

  /**
   * Inserts a token into the trie.
   */
  insert(word: string, score: number, index: number) {
    let node = this.root;

    const symbols = stringToChars(word);

    for (let i = 0; i < symbols.length; i++) {
      if (!node.children[symbols[i]]) {
        node.children[symbols[i]] = new TrieNode();
        node.children[symbols[i]].parent = node;
        node.children[symbols[i]].word[0] = node.word[0].concat(symbols[i]);
      }

      node = node.children[symbols[i]];
      if (i === symbols.length - 1) {
        node.end = true;
        node.word[1] = score;
        node.word[2] = index;
      }
    }
  }

  /**
   * Returns an array of all tokens starting with ss.
   *
   * @param ss The prefix to match on.
   */
  commonPrefixSearch(ss: string[]): OutputNode[] {
    const output: OutputNode[] = [];
    let node = this.root.children[ss[0]];

    for (let i = 0; i < ss.length && node; i++) {
      if (node.end) {
        output.push(node.word);
      }
      node = node.children[ss[i + 1]];
    }

    if (!output.length) {
      output.push([[ss[0]], 0, 0]);
    }

    return output;
  }
}