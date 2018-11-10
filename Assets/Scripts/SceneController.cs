using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;

    public const int rows = 2;
    public const int columns = 4;
    public const float offSetX = 2f;
    public const float offSetY = 2.5f;

    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    public int _score;
    public float m_waitTime = .5f;

    public bool canReveal(){
        return _secondRevealed == null;
    }

    public void revealCard(MemoryCard card){
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else {
            _secondRevealed = card;
            StartCoroutine(CheckMatch(_firstRevealed, _secondRevealed));
        }
    }

    private void updateScore(){
        scoreLabel.text = System.String.Format("Score: {0}", _score.ToString());
    }

    private IEnumerator CheckMatch(MemoryCard cd1, MemoryCard cd2){
        bool match = (cd1.id == cd2.id);
        if(match){
            _score++;
            updateScore();
            Debug.Log(System.String.Format("MATCH: {0} & {1}", cd1.ToString(), cd2.ToString()));
        } else {
            _score--;
            updateScore();
            yield return new WaitForSeconds(m_waitTime);
            cd1.hide();
            cd2.hide();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }

    private int[] createDeck(int numRows, int numColumns){
        int totalCards = numRows * numColumns;
        int numTypes = images.Length;
        int[] deck = new int[totalCards];

        for (int i = 0; i < totalCards; i++){
            deck[i] = i % numTypes;
        }

        int[] shuffledDeck = shuffle(deck);

        return shuffledDeck;
    }

    private int[] shuffle(int[] input){
        int[] newDeck = input.Clone() as int[];

        for (int i = 0; i < newDeck.Length; i++){
            int tmp = newDeck[i];
            int swpIdx = Random.Range(0, newDeck.Length);
            newDeck[i] = newDeck[swpIdx];
            newDeck[swpIdx] = tmp;
        }

        return newDeck;
    }

    private void createGrid(){
        int[] deck = createDeck(rows, columns);

        Vector3 startPos = originalCard.transform.position;
        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++){
                MemoryCard card;
                if(i==0 && j == 0){
                    card = originalCard;
                } else {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                // Determine card type to assign from shuffled deck
                int deckIdx = (i * rows) + j;
                int typeId = deck[deckIdx];
                card.SetCard(typeId, images[typeId]);

                float posX = (offSetX * i) + startPos.x;
                float posY = -(offSetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    // Use this for initialization
    void Start () {
        createGrid();
	}
	
	// Update is called once per frame
	void Update () {}

    public void Restart(){
        Application.LoadLevel("Memory");
    }
}
