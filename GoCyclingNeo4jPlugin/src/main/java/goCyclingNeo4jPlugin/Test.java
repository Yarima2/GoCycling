package goCyclingNeo4jPlugin;

import java.util.stream.Stream;
import java.util.stream.StreamSupport;

import org.neo4j.graphdb.Label;
import org.neo4j.graphdb.Node;
import org.neo4j.graphdb.Path;
import org.neo4j.graphdb.ResourceIterator;
import org.neo4j.graphdb.Transaction;
import org.neo4j.graphdb.traversal.Evaluators;
import org.neo4j.graphdb.traversal.Traverser;
import org.neo4j.logging.Log;
import org.neo4j.procedure.Context;
import org.neo4j.procedure.Description;
import org.neo4j.procedure.Mode;
import org.neo4j.procedure.Name;
import org.neo4j.procedure.Procedure;

public class Test {

		static final String TILE_LABEL = "TileConquer";
	
		static final Label TILE = Label.label(TILE_LABEL);
	
	
	 	@Context
	    public Transaction tx;

	    @Context
	    public Log log;
	    
	    
	    /**
	     * Uses the Traversal API to return all Person fond by a Depth of 2.
	     * This could be much easier with a simple Cypher statement, but serves as a demo onl.
	     * @param actorName name of the Person node to start from
	     * @return Stream of Person Nodes
	     */
	    @Procedure(name = "travers.findCoActors", mode = Mode.WRITE)
	    @Description("traverses starting from the Person with the given name and returns all co-actors")
	    public Stream<CoActorRecord> findCoActors() {

	        ResourceIterator<Node> nodeMinX =  tx.execute("MATCH (T:"+TILE_LABEL+") RETURN T ORDER BY T.x LIMIT 1").columnAs("T");
	        Node start = nodeMinX.next();

	        final Traverser traverse = BorderTraversal.createTraverser(start, tx);

	        return StreamSupport
	                .stream(traverse.spliterator(), false)
	                .map(Path::endNode)
	                .map(CoActorRecord::new);
	    }
	    
	    
	    /**
	     * See <a href="https://neo4j.com/docs/java-reference/4.2/javadocs/org/neo4j/procedure/Procedure.html">Procedure</a>
	     * <blockquote>
	     * A procedure must always return a Stream of Records, or nothing. The record is defined per procedure, as a class
	     * with only public, non-final fields. The types, order and names of the fields in this class define the format of the
	     * returned records.
	     * </blockquote>
	     * This is a record that wraps one of the valid return types (in this case a {@link Node}.
	     */
	    public static final class CoActorRecord {

	        public final Node node;

	        CoActorRecord(Node node) {
	            this.node = node;
	        }
	    }
}
