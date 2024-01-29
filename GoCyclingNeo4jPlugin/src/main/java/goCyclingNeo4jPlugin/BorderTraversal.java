package goCyclingNeo4jPlugin;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.javatuples.Pair;
import org.neo4j.graphdb.Node;
import org.neo4j.graphdb.Path;
import org.neo4j.graphdb.PathExpander;
import org.neo4j.graphdb.Relationship;
import org.neo4j.graphdb.ResourceIterable;
import org.neo4j.graphdb.ResourceIterator;
import org.neo4j.graphdb.Transaction;
import org.neo4j.graphdb.traversal.BranchOrderingPolicy;
import org.neo4j.graphdb.traversal.BranchSelector;
import org.neo4j.graphdb.traversal.BranchState;
import org.neo4j.graphdb.traversal.Evaluation;
import org.neo4j.graphdb.traversal.TraversalBranch;
import org.neo4j.graphdb.traversal.TraversalContext;
import org.neo4j.graphdb.traversal.Traverser;
import org.neo4j.internal.helpers.collection.AbstractResourceIterable;

public class BorderTraversal {

	public static Traverser createTraverser(Node start, Transaction tx) {
		BorderTraversal borderTraversal = new BorderTraversal();
		return tx.traversalDescription()// .order(new LpcnBranchOrder()).evaluator(borderTraversal.new Evaluator())
				.expand(new LpcnExpander(tx)).traverse(start);

	}

	public static class LpcnState {
		public int id = 0;
		public long prevDirX;
		public long prevDirY;
		public Node startNode;
		public Node secondNode;
		public Node prevNode;
	}

	public static class LpcnRelationshipIterable extends AbstractResourceIterable<Relationship> {

		Transaction tx;

		String query;

		Map<String, Object> parameters;

		public LpcnRelationshipIterable(Transaction tx, String query, Map<String, Object> parameters) {
			this.tx = tx;
			this.query = query;
			this.parameters = parameters;
		}

		public LpcnRelationshipIterable(Transaction tx, String query) {
			this(tx, query, new HashMap<String, Object>());
		}

		@Override
		protected ResourceIterator<Relationship> newIterator() {
			return tx.execute(query, parameters).columnAs(null);
		}

	}

	public static class LpcnExpander implements PathExpander<LpcnState> {

		Transaction tx;

		private static Map<Pair<Integer, Integer>, List<Pair<Integer, Integer>>> directions = Map.ofEntries(
				Map.entry(Pair.with(-1, -1),
						List.of(Pair.with(-1, 0), Pair.with(-1, 1), Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0),
								Pair.with(1, -1), Pair.with(0, -1), Pair.with(-1, -1))),
				Map.entry(Pair.with(-1, 0),
						List.of(Pair.with(-1, 1), Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0), Pair.with(1, -1),
								Pair.with(0, -1), Pair.with(-1, -1), Pair.with(-1, 0))),
				Map.entry(Pair.with(-1, 1),
						List.of(Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0), Pair.with(1, -1), Pair.with(0, -1),
								Pair.with(-1, -1), Pair.with(-1, 0), Pair.with(-1, 1))),
				Map.entry(Pair.with(0, 1),
						List.of(Pair.with(1, 1), Pair.with(1, 0), Pair.with(1, -1), Pair.with(0, -1), Pair.with(-1, -1),
								Pair.with(-1, 0), Pair.with(-1, 1), Pair.with(0, 1))),
				Map.entry(Pair.with(1, 1),
						List.of(Pair.with(1, 0), Pair.with(1, -1), Pair.with(0, -1), Pair.with(-1, -1),
								Pair.with(-1, 0), Pair.with(-1, 1), Pair.with(0, 1), Pair.with(1, 1))),
				Map.entry(Pair.with(1, 0),
						List.of(Pair.with(1, -1), Pair.with(0, -1), Pair.with(-1, -1), Pair.with(-1, 0),
								Pair.with(-1, 1), Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0))),
				Map.entry(Pair.with(1, -1),
						List.of(Pair.with(0, -1), Pair.with(-1, -1), Pair.with(-1, 0), Pair.with(-1, 1),
								Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0), Pair.with(1, -1))),
				Map.entry(Pair.with(0, -1), List.of(Pair.with(-1, -1), Pair.with(-1, 0), Pair.with(-1, 1),
						Pair.with(0, 1), Pair.with(1, 1), Pair.with(1, 0), Pair.with(1, -1), Pair.with(0, -1))));

		String query = "Match ({x:$origX, y:$origY})-[a]->({x:$trg0X, y:$trg0Y}) " + "Return a " + "Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg1X, y:$trg1Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg2X, y:$trg2Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg3X, y:$trg3Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg4X, y:$trg4Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg5X, y:$trg5Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg6X, y:$trg6Y}) " + "Return a Union ALL "
				+ "Match ({x:$origX, y:$origY})-[a]->({x:$trg7X, y:$trg7Y}) " + "Return a " + "Limit 1";

		public LpcnExpander(Transaction tx) {
			this.tx = tx;
		}

		@Override
		public ResourceIterable<Relationship> expand(Path path, BranchState<LpcnState> branchState) {
			LpcnState state = branchState.getState() == null ? new LpcnState() : branchState.getState();
			Map<String, Object> queryParams = new HashMap<String, Object>();

			if (path.endNode() == null)
				throw new IllegalArgumentException("Path empty");

			path.endNode().setProperty("traversalID", state.id++);

			int prevDirX = -1;
			int prevDirY = 0;
			if (state.startNode == null) {
				state.startNode = path.endNode();
			} else if (state.secondNode == null) {
				state.secondNode = path.endNode();
				prevDirX = (int) ((Long) state.prevNode.getProperty("x") - (Long) path.endNode().getProperty("x"));
				prevDirY = (int) ((Long) state.prevNode.getProperty("y") - (Long) path.endNode().getProperty("y"));
			} else {
				prevDirX = (int) ((Long) state.prevNode.getProperty("x") - (Long) path.endNode().getProperty("x"));
				prevDirY = (int) ((Long) state.prevNode.getProperty("y") - (Long) path.endNode().getProperty("y"));
				// End state
				if (path.endNode() == state.secondNode && state.prevNode == state.startNode) {
					return new LpcnRelationshipIterable(tx, "Match (a) Return a Limit 0");
				}
			}

			List<Pair<Integer, Integer>> directionOrder = directions.get(Pair.with(prevDirX, prevDirY));
			queryParams.put("origX", path.endNode().getProperty("x"));
			queryParams.put("origY", path.endNode().getProperty("y"));
			for (int i = 0; i < directionOrder.size(); i++) {
				Pair<Integer, Integer> nextDir = directionOrder.get(i);
				queryParams.put("trg" + i + "X", (Long) path.endNode().getProperty("x") + nextDir.getValue0());
				queryParams.put("trg" + i + "Y", (Long) path.endNode().getProperty("y") + nextDir.getValue1());
			}
			state.prevNode = path.endNode();
			branchState.setState(state);
			return new LpcnRelationshipIterable(tx, query, queryParams);
		}

		@Override
		public PathExpander<LpcnState> reverse() {
			return null;
		}

	}

	public static class LpcnBranchOrder implements BranchOrderingPolicy {

		@Override
		public BranchSelector create(TraversalBranch startBranch, PathExpander expander) {
			return new LpcnBranchSelector(startBranch, expander);
		}

	}

	public static class LpcnBranchSelector implements BranchSelector {

		private TraversalBranch current;
		private final PathExpander<LpcnState> expander;
		int travId;

		LpcnBranchSelector(TraversalBranch startSource, PathExpander<LpcnState> expander) {
			this.current = startSource;
			this.expander = expander;
		}

		@Override
		public TraversalBranch next(TraversalContext metadata) {
			current.endNode().setProperty("travId", travId++);
			return current.next(expander, metadata);
		}

	}

	public class Evaluator implements org.neo4j.graphdb.traversal.Evaluator {

		@Override
		public Evaluation evaluate(Path path) {
			if (path.length() == 0) {
				return Evaluation.EXCLUDE_AND_CONTINUE;
			}

			return Evaluation.INCLUDE_AND_CONTINUE;
		}

	}

}
