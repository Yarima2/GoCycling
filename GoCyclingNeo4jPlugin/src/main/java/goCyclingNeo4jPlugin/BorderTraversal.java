package goCyclingNeo4jPlugin;

import org.neo4j.graphdb.Direction;
import org.neo4j.graphdb.Node;
import org.neo4j.graphdb.Path;
import org.neo4j.graphdb.RelationshipType;
import org.neo4j.graphdb.Transaction;
import org.neo4j.graphdb.traversal.Evaluation;
import org.neo4j.graphdb.traversal.Traverser;
import org.neo4j.logging.Log;
import org.neo4j.procedure.Context;

public class BorderTraversal {

	
	public static Traverser createTraverser(Node start, Transaction tx) {
		BorderTraversal borderTraversal = new BorderTraversal();
		return tx.traversalDescription()
				.breadthFirst()
				.evaluator(borderTraversal.new Evaluator())
				//.evaluator(borderTraversal.new PathLogger())
				.relationships(RelationshipType.withName("Neighbour"), Direction.OUTGOING)
				.traverse(start);
				
	}

	
	
	
	public class Evaluator implements org.neo4j.graphdb.traversal.Evaluator {
		
		Node prev;
		Node prevPrev;
		
		@Override
		public Evaluation evaluate(Path path) {
			if ( path.length() == 0 )
            {
                return Evaluation.EXCLUDE_AND_CONTINUE;
            }
			Node current = path.endNode();
			int prevDeltaX, prevDeltaY;
			if(prevPrev == null) {
				prevDeltaX = 1;
				prevDeltaY = 0;
			}
			else {
				
			}
			current.setProperty("prev", prev.toString());
			
			prevPrev = prev;
			prev = current;
			return Evaluation.INCLUDE_AND_CONTINUE;
		}

	}
	
	
	public final class PathLogger implements org.neo4j.graphdb.traversal.Evaluator {

		@Context
		public Log log;
		
	    @Override
	    public Evaluation evaluate(Path path) {
	        log.info(path.toString());
	        return Evaluation.INCLUDE_AND_CONTINUE;
	    }
	}
	

}
