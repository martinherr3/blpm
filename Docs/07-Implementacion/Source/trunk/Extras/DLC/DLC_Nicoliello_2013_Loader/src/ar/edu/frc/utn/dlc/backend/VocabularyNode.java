package ar.edu.frc.utn.dlc.backend;

import ar.edu.frc.utn.dlc.Exceptions.SpiderException;

class VocabularyNode {

    private long nr;
    private long maxTf;
    private String word;

    public String getWord() {
        return word;
    }

    public VocabularyNode(String word) {
        this.word = word;
    }

    public VocabularyNode(String word,long maxTF, long nr) {
        this.word = word;
        this.maxTf = maxTF;
        this.nr = nr;
    }
    public long getNr() {
        return nr;
    }

    public long getMaxTf() {
        return maxTf;
    }

    protected void setMaxTf(long maxTf) throws SpiderException {
        if (this.maxTf < maxTf) {
            this.maxTf = maxTf;
        }
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null) {
            return false;
        }
        if (getClass() != obj.getClass()) {
            return false;
        }
        final VocabularyNode other = (VocabularyNode) obj;
        if ((this.word == null) ? (other.word != null) : !this.word.equals(other.word)) {
            return false;
        }
        return true;
    }

    @Override
    public int hashCode() {
        return this.word.hashCode();
    }

    protected void incNr(long nr) {
        this.nr += nr;
    }
}
